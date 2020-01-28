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
    class StaticOneParameter : BaseAnalyzer
    {
        public StaticOneParameter(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var staticMethods = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(method =>
                method.Modifiers.Where(modifier =>
                    modifier.Kind() == SyntaxKind.StaticKeyword)
                .Any());

            List<Warning> warnings = new List<Warning>();
            foreach(var method in staticMethods)
            {
                int count = method.ParameterList.Parameters.Count;
                if (count < 1)
                {
                    warnings.Add(new Warning(
                        "You got static methods without parameters at line: " + GetLineNumber(syntaxTree, method)+"\n"
                        ));

                }
            }

            return new AnalyzeResult(IssueDetail.StaticNoParameters, warnings);


        }
    }
}
