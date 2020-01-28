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
    class SingleAssertPerTest : BaseAnalyzer
    {
        public SingleAssertPerTest(SyntaxTree syntaxTree) : base(syntaxTree)
        {

        }

        public override AnalyzeResult analyze()
        {
            string warning_format = "Test method named {0} at line {1} is asserting more than one thing";
            var root = syntaxTree.GetRoot();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            int count = 0;

            List<Warning> warnings = new List<Warning>();

            foreach (MethodDeclarationSyntax method in methods)
            {
                if (method.Identifier.ToString().Contains("Test"))
                {
                    var assertCount = Regex.Matches(method.Body.ToString(), "Assert").Count;
                    if (assertCount > 1)
                    {
                        warnings.Add(new Warning(
                            String.Format(warning_format, method.Identifier, GetLineNumber(syntaxTree, method))
                            ));
                    }
                }
            }

            return new AnalyzeResult(IssueDetail.SingleAssertPerTest, warnings);
        }
    }
}
