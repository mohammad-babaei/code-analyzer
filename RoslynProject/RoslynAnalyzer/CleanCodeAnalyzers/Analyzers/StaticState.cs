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
    class StaticState : BaseAnalyzer
    {
        public StaticState(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var staticFields = root.DescendantNodes()
            .OfType<FieldDeclarationSyntax>()
            .Where(method =>
                method.Modifiers.Where(modifier =>
                    modifier.Kind() == SyntaxKind.StaticKeyword)
                .Any());
            List<Warning> warnings = new List<Warning>();

            foreach(var field in staticFields)
            {
                warnings.Add(new Warning(
                    "You have static field declaration at line : "+GetLineNumber(syntaxTree, field)
                    ));
            }

            return new AnalyzeResult(IssueDetail.StaticState, warnings);

        }
    }
}
