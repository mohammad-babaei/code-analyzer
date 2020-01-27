using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Analyzers
{
    class LineCount : BaseAnalyzer
    {
        public LineCount(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var text = syntaxTree.GetText().ToString();

            var lineCount = Regex.Matches(text, "\r").Count;

            string format_warning = "Total file lines: {0}";
            List<Warning> warnings = new List<Warning>
            {
                new Warning(String.Format(format_warning, lineCount))
            };
            return new AnalyzeResult(IssueDetail.LineCount, warnings);
        }
    }
}
