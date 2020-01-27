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
    class FunctionArgCounter : BaseAnalyzer
    {
        public FunctionArgCounter(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override AnalyzeResult analyze()
        {
            var root = this.syntaxTree.GetRoot();
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            int zero_arg = 0;
            int one_arg = 0;
            int two_arg = 0;
            int three_arg = 0;

            List<Warning> warnings = new List<Warning>();

            foreach (var method in methods)
            {
                var arg_count = method.ParameterList.Parameters.ToList().Count;
                if(arg_count == 0)
                {
                    zero_arg++;
                }else if(arg_count == 1)
                {
                    one_arg++;
                }
                else if (arg_count == 2)
                {
                    two_arg++;
                }
                else if (arg_count == 3)
                {
                    three_arg++;
                }


            }
            warnings.Add(new Warning(
                            "You have " + zero_arg + " methods with zero arguments\nYou have " + one_arg + " methods with one arguments\nYou have " + two_arg + " methods with two arguments\nYou have " + three_arg + " funcs with three arguments\n"
                        ));

            return new AnalyzeResult(IssueDetail.FunctionArgCounters, warnings);
        }
    }
}
