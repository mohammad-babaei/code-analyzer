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
    class BooleanParameterAnalyzer : BaseAnalyzer
    {

        string warning_format = "Method named {0} at line {1} has boolean parameters";

        public BooleanParameterAnalyzer(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            List<Warning> warnings = new List<Warning>();

            foreach (MethodDeclarationSyntax method in methods)
            {
                var parameters = method.ParameterList.Parameters.ToList();

                foreach (var param in parameters)
                {
                    PredefinedTypeSyntax type = (PredefinedTypeSyntax) param.Type;
                    if (type.Keyword.Value == "bool")
                    {
                        warnings.Add(new Warning(String.Format(warning_format, method.Identifier, GetLineNumber(syntaxTree, method))));
                        break;
                    }
                }
            }
            return new AnalyzeResult(
                IssueDetail.BooleanParameter,
                warnings
                );
        }
    }
}
