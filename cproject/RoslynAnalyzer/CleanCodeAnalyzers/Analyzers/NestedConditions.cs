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
    class NestedConditions : BaseAnalyzer
    {
        public NestedConditions(SyntaxTree syntaxTree) : base(syntaxTree)
        {

        }

        public override AnalyzeResult analyze()
        {
            string warning_format = "Nested condition with 3 or more levels was found at line {0}";
            var root = syntaxTree.GetRoot();
            var ifStatementSyntaxes = root.DescendantNodes().OfType<IfStatementSyntax>().ToList();

            List<Warning> warnings = new List<Warning>();

            foreach (var ifStatement in ifStatementSyntaxes)
            {
                bool foundNested = false;
                foreach (var ifSt2 in ifStatement.DescendantNodes().OfType<IfStatementSyntax>())
                {
                    foreach (var ifSt3 in ifSt2.DescendantNodes().OfType<IfStatementSyntax>())
                    {
                        foundNested = true;
                    }
                }

                if (foundNested)
                {
                    warnings.Add(new Warning(
                            String.Format(warning_format, GetLineNumber(syntaxTree, ifStatement))
                        ));
                }
            }

            return new AnalyzeResult(IssueDetail.NestedConditions, warnings);
        }
    }
}
