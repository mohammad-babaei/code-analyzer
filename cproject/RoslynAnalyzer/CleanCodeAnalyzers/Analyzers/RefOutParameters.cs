using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Analyzers
{
    class RefOutParameters : BaseAnalyzer
    {
        public RefOutParameters(SyntaxTree syntaxTree) : base(syntaxTree)
        {

        }
        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var Methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();


            List<Warning> warnings = new List<Warning>();
            foreach (var method in Methods)
            {
                if(method.ParameterList.ToString().Contains("out ") || method.ParameterList.ToString().Contains("ref "))
                {
                    warnings.Add(new Warning(
                        "You got methods with 'out' or 'ref' keyword at line: " + GetLineNumber(syntaxTree, method) + "\n"
                        ));
                }
                
            }


            return new AnalyzeResult(IssueDetail.RefOutParameter, warnings);


        }
    }
}
