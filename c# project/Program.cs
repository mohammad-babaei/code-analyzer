using CocoCompiler2;
using System;

namespace at.jku.ssw.Coco
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = args[0];
            TokenGenerator generator = new TokenGenerator(fileName);
            var tokenEntities = generator.GetAllTokens();
            foreach (TokenEntity token in tokenEntities)
            {
                Console.WriteLine(token.ToString());
            }
            Console.WriteLine("-------------------------------------------------");

            CleanCodeAnalyzer analyzer = new CleanCodeAnalyzer(generator);

            var longMethods = analyzer.FindLongMethods();

            if (longMethods.Count == 0) {
                Console.WriteLine("No long methods were found. Good job!");
            } else
            {
                foreach (TokenEntity longMethodToken in longMethods)
                {
                    Console.WriteLine("Long method found at (" + longMethodToken.Line + ", " + longMethodToken.Col + ")");
                }
            }

            Console.WriteLine("-------------------------------------------------");

            var nonEnglish = analyzer.FindNonEnglishIdentifiers();

            if (nonEnglish.Count == 0)
            {
                Console.WriteLine("No non-English identifiers were found. Good job!");
            } else
            {
                foreach (TokenEntity token in nonEnglish)
                {
                    Console.WriteLine("Non-English identifier " + token.Value + " at (" + token.Line + ", " + token.Col + ")");
                }
            }
            
            Console.WriteLine("-------------------------------------------------");
            
            var longs = analyzer.FindExcessiveMethodParameter();

            if (longs.Count == 0)
            {
                Console.WriteLine("No method with excessive number of parameter was found. Good job!");
            } else
            {
                foreach (TokenEntity parameterToken in longs)
                {
                    Console.WriteLine("Method with too many params at (" + parameterToken.Line + ", " + parameterToken.Col);
                }
            }
        }
    }
}
