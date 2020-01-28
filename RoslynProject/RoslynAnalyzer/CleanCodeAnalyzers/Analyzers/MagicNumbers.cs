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
    class MagicNumbers : BaseAnalyzer
    {
        string warning_format = "Magic number {0} was found at line {1} is asserting more than one thing";

        public MagicNumbers(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = syntaxTree.GetRoot();
            var numbers = root.DescendantNodes().OfType<LiteralExpressionSyntax>()
                .Where(d => d.Token.Value is int && (int)d.Token.Value > 3).ToList();

            List<Warning> warnings = new List<Warning>();

            
            foreach (var num in numbers)
            {
                warnings.Add(new Warning(
                    String.Format(warning_format, num.Token.Value, GetLineNumber(syntaxTree, num))
                    ));
            }

            return new AnalyzeResult(IssueDetail.MagicNumbers, warnings);
        }
    }
}
