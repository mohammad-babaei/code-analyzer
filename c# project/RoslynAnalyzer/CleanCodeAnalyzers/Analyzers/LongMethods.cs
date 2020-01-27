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
    class LongMethods : BaseAnalyzer
    {
        private string warning_format = "Method named {0} at line {1} is way too long with {2} lines";

        public LongMethods(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var MethodDeclarationSyntaxes = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            List<Warning> warnings = new List<Warning>();

            foreach (var methodDec in MethodDeclarationSyntaxes)
            {
                FileLinePositionSpan lineSpan = methodDec.SyntaxTree.GetLineSpan(methodDec.Span);

                int lineCount = lineSpan.EndLinePosition.Line - lineSpan.StartLinePosition.Line + 1;

                if (lineCount > 10)
                {
                    warnings.Add(new Warning(
                        String.Format(warning_format, methodDec.Identifier, GetLineNumber(syntaxTree, methodDec), lineCount)
                    ));
                }

            }

            return new AnalyzeResult(IssueDetail.LongMethods, warnings);
        }
    }
}
