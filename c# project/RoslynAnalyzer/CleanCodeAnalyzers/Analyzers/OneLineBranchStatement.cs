using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Analyzers.OneLineBranchStatement
{
    class OneLineBranchStatement : BaseAnalyzer
    {
        public OneLineBranchStatement(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var ifStatements = root.DescendantNodes().OfType<IfStatementSyntax>().ToList();
            var WhileStatements = root.DescendantNodes().OfType<WhileStatementSyntax>().ToList();

            List<Warning> warnings = new List<Warning>();

            foreach (var ifst in ifStatements)
            {
                var block = ifst.ChildNodes().OfType<BlockSyntax>().FirstOrDefault();
                if (block != null)
                {
                    var statements = block.Statements.ToList().Count;
                    if (statements > 1)
                    {
                        warnings.Add(new Warning(
                            "Your if statement " + "at line " + GetLineNumber(syntaxTree, ifst) + " has more than 2 lines of code "
                        ));
                    }
                }

            }
            foreach (var whilest in WhileStatements)
            {
                var block = whilest.ChildNodes().OfType<BlockSyntax>().FirstOrDefault();
                if (block != null)
                {
                    var statements = block.Statements.ToList().Count;
                    if (statements > 1)
                    {
                        warnings.Add(new Warning(
                            "Your while statement " + "at line " + GetLineNumber(syntaxTree, whilest) + " has more than 2 lines of code "
                        ));
                    }
                }
            }
            return new AnalyzeResult(IssueDetail.OneLineBranchStatements, warnings);
        }
    }
}
