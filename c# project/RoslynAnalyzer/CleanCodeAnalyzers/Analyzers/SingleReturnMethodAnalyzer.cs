using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Analyzers
{
    class SingleReturnMethodAnalyzer : BaseAnalyzer
    {
        public SingleReturnMethodAnalyzer(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            int count = 0;

            List<Warning> warnings = new List<Warning>();

            foreach (MethodDeclarationSyntax method in methods)
            {
                var returnCount = Regex.Matches(method.Body.ToString(), "return ").Count;
                warnings.Add(new Warning(
                    "Your method " + method.Identifier + " at line " + GetLineNumber(syntaxTree, method) + " has " + returnCount + " return statements"
                ));
            }
            return new AnalyzeResult(IssueDetail.SingleReturnFunctions, warnings);
        }

    }
}
