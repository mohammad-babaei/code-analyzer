using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using at.jku.ssw.Coco;
using CocoCompiler2.Data;

namespace CocoCompiler2
{
    class CleanCodeAnalyzer
    {
        TokenGenerator tokenGenerator;
        NetSpell.SpellChecker.Dictionary.WordDictionary oDict;
        NetSpell.SpellChecker.Spelling oSpell;

        public CleanCodeAnalyzer(TokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
            oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();
            //If you're running this project separately you need to change this to relative path.
            oDict.DictionaryFile = "CocoProject\\packages\\NetSpell.2.1.7\\dic\\en-US.dic";
            oDict.Initialize();
            oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
        }

        public List<TokenEntity> FindLongMethods()
        {
            var tokens = tokenGenerator.GetAllTokens();
            List<TokenEntity> bracketTokens = tokenGenerator.GetAllTokens()
                .Where(token => token.Value == "{" || token.Value == "}").ToList();
            return LongMethodUtil(bracketTokens);
        }

        public AnalyzeResult FindNonEnglishIdentifiers()
        {
            string warning_format = "Non-English Identifier {0} was found at ({1},{2})";
            List<TokenEntity> tokens = tokenGenerator.GetAllTokens();
            var nonEnglishTokens= tokens.Where(token => !ContainsEnglishWord(token.Value) && token.Kind == "Identifier").ToList();
            var warnings = nonEnglishTokens.Select(token =>
                new Warning(String.Format(warning_format, token.Value, token.Line, token.Col))).ToList();
            return new AnalyzeResult(IssueDetail.meaningfulNames, warnings);
        }

        public AnalyzeResult FindExcessiveMethodParameter()
        {
            List<Tuple<TokenEntity,TokenEntity>> brackets = new List<Tuple<TokenEntity, TokenEntity>>();
            var tokens = tokenGenerator.GetAllTokens();

            for (int i = 0; i < tokens.Count; i++)
            {
                for (int j = 0; j < tokens.Count; j++)
                {
                    if (tokens[i].Value == "(" && tokens[j].Value == ")")
                    {
                        if (tokens[i].Line == tokens[j].Line && tokens[i].Col < tokens[j].Col)
                        {
                            brackets.Add(Tuple.Create(tokens[i], tokens[j]));
                        } 
                    }
                }
            }

            List<TokenEntity> delims = tokenGenerator.GetAllTokens().Where(token => token.Value == ",").ToList();

            Dictionary<Tuple<TokenEntity, TokenEntity>, int> parameterCount;
            parameterCount = new Dictionary<Tuple<TokenEntity, TokenEntity>, int>();

            foreach (TokenEntity delimToken in delims)
            {
                foreach (Tuple<TokenEntity,TokenEntity> bracketPair in brackets)
                {
                    if (delimToken.Line == bracketPair.Item1.Line)
                    {
                        if (delimToken.Col > bracketPair.Item1.Col && delimToken.Col < bracketPair.Item2.Col)
                        {
                            if (parameterCount.ContainsKey(bracketPair))
                            {
                                parameterCount[bracketPair]++;
                            } else
                            {
                                parameterCount[bracketPair] = 1;
                            }
                        }
                    }
                }
            }
            List<TokenEntity> result = new List<TokenEntity>();
            foreach (Tuple<TokenEntity, TokenEntity> bracketTokenPair in brackets)
            {
                //Console.WriteLine("Tokens a-> " + bracketTokenPair.Item1.ToString() + "   " + "b -> " + bracketTokenPair.Item2.ToString());
                if (!parameterCount.ContainsKey(bracketTokenPair))
                {
                    continue;
                }
                int count = parameterCount[bracketTokenPair];
                if (count > 4)
                {
                    result.Add(bracketTokenPair.Item1);
                }
            }

            var warning_format = "Method with too many params at ({0},{1})";

            var warnings = result.Select(token => new Warning(String.Format(warning_format, token.Line, token.Col)))
                .ToList();

            return new AnalyzeResult(IssueDetail.excessiveParameters, warnings);
        }
        private List<TokenEntity> LongMethodUtil(List<TokenEntity> bracketTokens)
        {
            Stack<TokenEntity> S = new Stack<TokenEntity>();
            List<TokenEntity> longTokens = new List<TokenEntity>();
            foreach (TokenEntity t in bracketTokens)
            {
                if (t.Value == "{")
                {
                    S.Push(t);
                }
                else
                {
                    TokenEntity topToken = S.Pop();
                    int lineDiff = t.Line - topToken.Line;
                    if (lineDiff > 24)
                        longTokens.Add(topToken);
                }
            }
            return longTokens;
        }

        private bool ContainsEnglishWord(string wordToCheck)
        {
            if (IsEnglishWord(wordToCheck))
            {
                return true;
            }
            var stringList = new List<string>();
            for (int i = 0; i < wordToCheck.Length; i++)
                for (int j = i; j < wordToCheck.Length; j++)
                    stringList.Add(wordToCheck.Substring(i, j - i + 1));

            return stringList.Any(word => IsEnglishWord(word) && word.Length >= 3);
        }

        private bool IsEnglishWord(string wordToCheck)
        {
            return oSpell.TestWord(wordToCheck);
        }
    }
}
