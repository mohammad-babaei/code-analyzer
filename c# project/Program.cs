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
            CleanCodeAnalyzer analyzer = new CleanCodeAnalyzer(generator);

            var longMethods = analyzer.FindLongMethods();

            if (longMethods.Count == 0) {
                Console.WriteLine("No methods with more than 24 lines were found");
            } else
            {
                foreach (TokenEntity longMethodToken in longMethods)
                {
                    Console.WriteLine("You have a long method at (" + longMethodToken.Line + ", " + longMethodToken.Col + ")");
                }
            }
        }
        
    }
}
