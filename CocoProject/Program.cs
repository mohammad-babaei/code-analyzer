using CocoCompiler2;
using System;
using System.Collections.Generic;
using CocoCompiler2.Data;

namespace at.jku.ssw.Coco
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = args[0];
            TokenGenerator generator = new TokenGenerator(fileName);
            var tokenEntities = generator.GetAllTokens();
            CleanCodeAnalyzer analyzer = new CleanCodeAnalyzer(generator);

            var parameters = analyzer.FindExcessiveMethodParameter();
            var nonEnglish = analyzer.FindNonEnglishIdentifiers();

            CodeDiagnosticResponse response = new CodeDiagnosticResponse(new List<AnalyzeResult>() {parameters, nonEnglish});
            Console.WriteLine(response);
        }
    }
}
