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
    class NestedLoops : BaseAnalyzer
    {
        public NestedLoops(SyntaxTree syntaxTree) : base(syntaxTree)
        {

        }

        public override AnalyzeResult analyze()
        {
            string warning_format = "Nested loop with 3 or more levels was found at line {0}";
            var root = syntaxTree.GetRoot();
            var ifStatementSyntaxes = root.DescendantNodes().OfType<ForStatementSyntax>().ToList();

            List<Warning> warnings = new List<Warning>();

            foreach (var forStatement in ifStatementSyntaxes)
            {
                bool foundNested = false;
                foreach (var forStatement2 in forStatement.DescendantNodes().OfType<ForStatementSyntax>())
                {
                    foreach (var forStatement3 in forStatement2.DescendantNodes().OfType<ForStatementSyntax>())
                    {
                        foundNested = true;
                    }
                }

                if (foundNested)
                {
                    warnings.Add(new Warning(
                        String.Format(warning_format, GetLineNumber(syntaxTree, forStatement))
                    ));
                }
            }

            return new AnalyzeResult(IssueDetail.NestedLoop, warnings);
        }
    }
}
