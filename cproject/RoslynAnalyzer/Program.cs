using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RoslynAnalyzer.CleanCodeAnalyzers.Analyzers;
using RoslynAnalyzer.CleanCodeAnalyzers.Analyzers.OneLineBranchStatement;
using RoslynAnalyzer.CleanCodeAnalyzers.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using RoslynAnalyzer.CleanCodeAnalyzers;

namespace RoslynAnalyzer
{
    
    class Program
    {
        static void Main(string[] args)
        {
            string text = File.ReadAllText(args[0]);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
            SingleReturnMethodAnalyzer analyzer = new SingleReturnMethodAnalyzer(tree);
            AnalyzeResult result = analyzer.analyze();

            // BooleanParameterAnalyzer booleanParameterAnalyzer = new BooleanParameterAnalyzer(tree);
            // AnalyzeResult result2 = booleanParameterAnalyzer.analyze();

 	        OneLineBranchStatement oneLineBranchStatement = new OneLineBranchStatement(tree);
            AnalyzeResult resultOneLineBranchStatement = oneLineBranchStatement.analyze();

            FunctionArgCounter functionArgCounter = new FunctionArgCounter(tree);
            AnalyzeResult resultfunctionArgCounter = functionArgCounter.analyze();

            NestedLoops nestedLoops = new NestedLoops(tree);
            AnalyzeResult nestedLoopResult = nestedLoops.analyze();

            CommentPercentage comment = new CommentPercentage(tree);
            AnalyzeResult resultcomment = comment.analyze();

            StaticNotVoid staticNotVoid = new StaticNotVoid(tree);
            AnalyzeResult resultstaticNotVoid = staticNotVoid.analyze();

            TryCatchFucntionCall tryCatchFucntionCall = new TryCatchFucntionCall(tree);
            AnalyzeResult resulttryCatchFucntionCall = tryCatchFucntionCall.analyze();

            SingleAssertPerTest singleAssertPerTest = new SingleAssertPerTest(tree);
            AnalyzeResult singleAssertAnalyzeResult = singleAssertPerTest.analyze();

            MagicNumbers magicNumbersAnalyzer = new MagicNumbers(tree);
            AnalyzeResult magicAnalyzeResult = magicNumbersAnalyzer.analyze();

            LongClasses longClasses = new LongClasses(tree);
            AnalyzeResult longClassesResult = longClasses.analyze();

            var analyzers = new List<BaseAnalyzer>()
            {
                new SingleReturnMethodAnalyzer(tree),

                // new BooleanParameterAnalyzer(tree),

                new OneLineBranchStatement(tree),

                new FunctionArgCounter(tree),

                new NestedLoops(tree),

                new CommentPercentage(tree),

                new StaticNotVoid(tree),

                new TryCatchFucntionCall(tree),

                new SingleAssertPerTest(tree),

                new MagicNumbers(tree),

                new LongClasses(tree),

                new EmptyCatch(tree),

                new NestedConditions(tree),

                new LongLine(tree),

                new LongMethods(tree),

                new LineCount(tree),

                new StaticOneParameter(tree),

                new StaticState(tree),

                new RefOutParameters(tree),

                new FuncContainAnd(tree)

        };

            var diagnoseResponse = CodeDiagnosticResponse.fromAnalyzers(
                analyzers
            );

            Console.WriteLine(diagnoseResponse);

            EmptyCatch emptyCatch = new EmptyCatch(tree);
            AnalyzeResult resultemptyCatch = emptyCatch.analyze();

            NestedConditions nestedConditions = new NestedConditions(tree);
            AnalyzeResult resultNested = nestedConditions.analyze();

            LongLine longLine = new LongLine(tree);
            AnalyzeResult resultlongLine = longLine.analyze();

            LongMethods longMethods = new LongMethods(tree);
            AnalyzeResult resultLongMethods = longMethods.analyze();

            LineCount lineCount = new LineCount(tree);
            AnalyzeResult lineCountResult = lineCount.analyze();

            StaticOneParameter staticOneParameter = new StaticOneParameter(tree);
            AnalyzeResult staticOneParatemerResult = staticOneParameter.analyze();

            StaticState staticState = new StaticState(tree);
            AnalyzeResult staticStateStaticResult = staticState.analyze();

            RefOutParameters refOutParameters = new RefOutParameters(tree);
            AnalyzeResult refOutParametersResult = refOutParameters.analyze();

            FuncContainAnd funcContainAnd = new FuncContainAnd(tree);
            AnalyzeResult funcContainAndResult = funcContainAnd.analyze();


            Console.Read();
            
        }
    }
}
