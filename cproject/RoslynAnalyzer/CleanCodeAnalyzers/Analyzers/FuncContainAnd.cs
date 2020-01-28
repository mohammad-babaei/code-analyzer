using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Analyzers
{
    class FuncContainAnd : BaseAnalyzer
    {
        public FuncContainAnd(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var Methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();


            List<Warning> warnings = new List<Warning>();
            foreach (var method in Methods)
            {
                if (method.Identifier.ToString().Contains("And"))
                {
                    warnings.Add(new Warning(
                        "You got methods with 'And' in identifire at line: " + GetLineNumber(syntaxTree, method) + "\n"
                        ));
                }

            }


            return new AnalyzeResult(IssueDetail.ContainsAnd, warnings);
        }
    }
}
