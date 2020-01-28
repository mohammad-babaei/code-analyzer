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
    class TryCatchFucntionCall : BaseAnalyzer
    {
        public TryCatchFucntionCall(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var tryStatements = root.DescendantNodes().OfType<TryStatementSyntax>().ToList();
            var catchStatements = root.DescendantNodes().OfType<CatchClauseSyntax>().ToList();

            List<Warning> warnings = new List<Warning>();

            foreach (var tryst in tryStatements)
            {
                var block = tryst.Block;
                if (block != null)
                {
                    var statements = block.Statements.ToList().Count;
                    if (statements > 1)
                    {
                        warnings.Add(new Warning(
                            "Your try statement " + "at line " + GetLineNumber(syntaxTree, tryst) + " is not called by a fucntion "
                        ));
                    }
                }

            }
            foreach (var catchst in catchStatements)
            {
                var block = catchst.Block;
                if (block != null)
                {
                    var statements = block.Statements.ToList().Count;
                    if (statements > 1)
                    {
                        warnings.Add(new Warning(
                            "Your catch statement " + "at line " + GetLineNumber(syntaxTree, catchst) + " is not called by a fucntion "
                        ));
                    }
                }
            }
            return new AnalyzeResult(IssueDetail.TryCatchFucntionCalls, warnings);
        }
    }
}
