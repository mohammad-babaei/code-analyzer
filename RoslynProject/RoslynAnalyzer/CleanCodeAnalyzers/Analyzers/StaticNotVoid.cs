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
    class StaticNotVoid : BaseAnalyzer
    {
        public StaticNotVoid(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }
        public void kos()
        {

        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var staticMethods = root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(method =>
                method.Modifiers.Where(modifier =>
                    modifier.Kind() == SyntaxKind.StaticKeyword)
                .Any());
            List<Warning> warnings = new List<Warning>();

            foreach (var method in staticMethods)
            {
                if(method.ToString().Contains("void "))
                {
                    warnings.Add(new Warning(
                "You have static method declared as void at line " + GetLineNumber(syntaxTree, method)));
                }
                
            }
            return new AnalyzeResult(IssueDetail.StaticNoParameters, warnings);
        }
    }
}
