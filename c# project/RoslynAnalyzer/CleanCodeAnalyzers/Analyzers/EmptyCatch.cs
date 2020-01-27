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
    class EmptyCatch : BaseAnalyzer
    {
        public EmptyCatch(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var catchStatements = root.DescendantNodes().OfType<CatchClauseSyntax>().ToList();
            List<Warning> warnings = new List<Warning>();

            foreach (var catchst in catchStatements)
            {
                var block = catchst.Block;
                if (block != null)
                {
                    var statements = block.Statements.ToList().Count;
                    if (statements == 0)
                    {
                        warnings.Add(new Warning(
                            "Your catch statement " + "at line " + GetLineNumber(syntaxTree, catchst) + " is empty and does nothing "
                        ));
                    }
                }
            }
            return new AnalyzeResult(IssueDetail.EmptyCatches, warnings);
        }

    }
}
