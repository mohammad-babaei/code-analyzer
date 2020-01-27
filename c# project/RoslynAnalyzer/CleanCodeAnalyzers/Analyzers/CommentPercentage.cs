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
    class CommentPercentage : BaseAnalyzer
    {
        public CommentPercentage(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var commentNodes = from node in root.DescendantTrivia() where node.IsKind(SyntaxKind.MultiLineCommentTrivia) || node.IsKind(SyntaxKind.SingleLineCommentTrivia) select node;
            int totoal_comment_line_count = 0;
            foreach(var comnode in commentNodes)
            {
                int lines = comnode.ToString().Split('\n').Length - 1;
                totoal_comment_line_count += lines;
            }var kos = root.DescendantNodesAndSelf().OfType<CompilationUnitSyntax>();
            var total_code_lines = root.DescendantNodesAndSelf().OfType<CompilationUnitSyntax>().First().ToString().Split('\n').Length - 1;

            List<Warning> warnings = new List<Warning>();
            warnings.Add(new Warning(
                "your total code is made of " + (total_code_lines + 1) + " lines and you have " + (totoal_comment_line_count + 1) + " lines of comment"
                ));
            

            return new AnalyzeResult(IssueDetail.CodeCommentPercentage, warnings);

        }
    }
}
