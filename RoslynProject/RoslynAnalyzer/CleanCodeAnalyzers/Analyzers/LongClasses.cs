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
    class LongClasses : BaseAnalyzer
    {
        private string warning_format = "Class named {0} at line {1} is way too long with {2} lines";

        public LongClasses(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var classDeclarationSyntaxes= root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            List<Warning> warnings = new List<Warning>();

            foreach (var classDec in classDeclarationSyntaxes)
            {
                FileLinePositionSpan lineSpan = classDec.SyntaxTree.GetLineSpan(classDec.Span);
                
                int lineCount = lineSpan.EndLinePosition.Line - lineSpan.StartLinePosition.Line + 1;

                if (lineCount > 10)
                {
                    warnings.Add(new Warning(
                            String.Format(warning_format, classDec.Identifier, GetLineNumber(syntaxTree, classDec), lineCount)                    
                        ));
                }

            }

            return new AnalyzeResult(IssueDetail.LongClasses, warnings);
        }
    }
}
