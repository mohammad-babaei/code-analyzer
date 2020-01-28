using at.jku.ssw.Coco;
using System.Collections.Generic;

namespace CocoCompiler2
{
    class TokenGenerator
    {
        static Dictionary<int, string> kinds = new Dictionary<int, string> {
            { 1, "Identifier" },
            { 41, "operator" },
            { 100, "semicolon" },
            { 30, "openning parantheses" },
            { 31, "closing parantheses" },
            { 34, "openning curly brackets" },
            { 35, "closing curly brackets" },
            { 3, "string literal" },
            { 18, "dot" },
            { 2, "integer" },
            { 17, "assignment operator" },
        };

        static List<TokenEntity> cacheTokens = null;

        private string fileName;
        private Scanner scanner;

        public TokenGenerator(string fileName)
        {
            this.fileName = fileName;
            this.scanner = new Scanner(fileName);
        }

        public List<TokenEntity> GetAllTokens()
        {
            if (cacheTokens != null)
            {
                return cacheTokens;
            }

            List<TokenEntity> result = new List<TokenEntity>();

            int index = 1;

            Token t = scanner.clean(scanner.NextToken());
            while (t.kind != 0)
            {
                result.Add(FromToken(scanner.clean(t), index));
                index++;
                t = scanner.clean(scanner.NextToken());
            }
            cacheTokens = result;
            return result;
        }


        private TokenEntity FromToken(Token token, int id)
        {
            string kind = "" + token.kind;
            if (kinds.ContainsKey(token.kind))
            {
                kind = kinds[token.kind];
            }
            TokenEntity tokenEntity = new TokenEntity(id, token.line, token.col, kind, token.val);
            return tokenEntity;
        }
    }
}
