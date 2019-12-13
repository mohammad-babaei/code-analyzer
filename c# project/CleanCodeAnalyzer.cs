using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoCompiler2
{
    class CleanCodeAnalyzer
    {
        TokenGenerator tokenGenerator;

        public CleanCodeAnalyzer(TokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
        }

        public List<TokenEntity> FindLongMethods()
        {
            var tokens = tokenGenerator.GetAllTokens();
            List<TokenEntity> bracketTokens = tokenGenerator.GetAllTokens()
                .Where(token => token.Value == "{" || token.Value == "}").ToList();
            return LongMethodUtil(bracketTokens);
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

    }
}
