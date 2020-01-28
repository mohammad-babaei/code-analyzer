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
    class LongLine : BaseAnalyzer
    {
        public LongLine(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var total_lines = root.DescendantNodesAndSelf().OfType<CompilationUnitSyntax>().First().ToString().Split('\n');
            List<Warning> warnings = new List<Warning>();

            for (var i = 0; i < total_lines.Length; i++)
            {
                if(total_lines[i].Length>100)
                {
                    warnings.Add(new Warning(
                        "Long line at row " + (i+1)
                        ));
                }
            }

            return new AnalyzeResult(IssueDetail.LongLines, warnings);

        }
    }
}
