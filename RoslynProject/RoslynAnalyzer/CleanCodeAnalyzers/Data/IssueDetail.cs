using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Data
{
    class IssueDetail
    {
        public int id;
        public string quote;
        public string author;
        public string title;
        public string type;
        const string BOB = "Robert C. Martin";

        public IssueDetail(int id, string quote, string author, string title, string type)
        {
            this.id = id;
            this.quote = quote;
            this.author = author;
            this.title = title;
            this.type = type;
        }

        public static IssueDetail SingleReturnFunctions = new IssueDetail(
            id: 1,
            quote: "A function should do one thing, and only one thing.",
            author: BOB,
            title: "Your code has methods with multiple returns in these methods:",
            type: "Single Return Functions"
            );

        public static IssueDetail BooleanParameter = new IssueDetail(
            id: 2,
            quote: "The main issue with a method which has a boolean parameter is that it forces the method body to handle logic it should have been told about.",
            author: BOB,
            title: "Your code has methods with boolean parameters in here:",
            type: "Boolean Parameters"
            );
        public static IssueDetail FunctionArgCounters = new IssueDetail(
            id: 3,
            quote: "The ideal number of arguments for a function is zero(nomadic), Next comes one(monadic), followed closely by two (dyadic). Three arguments(triadic) should be avoided where possible. More than three(polyadic) requires very special justification — and then shouldn’t be used anyway.",
            author: BOB,
            title: "Method argument counts:",
            type: "Method Argument Count"
            );
        public static IssueDetail OneLineBranchStatements = new IssueDetail(
            id: 4,
            quote: "This implies that the blocks within if statements, _else_ statements, _while_ statements, and so on should be one line long.",
            author: BOB,
            title: "If blocks with multi line:",
            type: "Single line branch statements"
        );

        public static IssueDetail NestedLoop = new IssueDetail(
            id: 5,
            quote: "Nested loops are frequently (but not always) bad practice.",
            author: "",
            title: "Nested loops were found:",
            type: "Nested for loops"
            );

        public static IssueDetail TryCatchFucntionCalls = new IssueDetail(
            id: 6,
            quote: "Try/Catch blocks are ugly in their own right. They confuse the structure of the code and mix error processing with normal processing. So it is better to extract the bodies of the try and catch blocks out into functions of their own.",
            author: BOB,
            title: "Your code has methods with boolean parameters in here:",
            type: "Try Catch Function Calls"
            );

        public static IssueDetail SingleAssertPerTest = new IssueDetail(
            id: 7,
            quote: "We should write our tests, trying to assert one concept per unit test.",
            author: BOB,
            title: "You have test methods that assert more than one thing:",
            type: "Single Assert per Test"
            );
       
        public static IssueDetail CodeCommentPercentage = new IssueDetail(
            id: 8,
            quote: " It is well known that I prefer code that has few comments. I code by the principle that good code does not require many comments",
            author: BOB,
            title: "",
            type: "Code Comment Percentage"
            );
        public static IssueDetail StaticVoid = new IssueDetail(
            id: 9,
            quote: "Better to not declare static methods as void",
            author: BOB,
            title: "",
            type: "Static void methods"
            );

        public static IssueDetail MagicNumbers = new IssueDetail(
            id: 10,
            quote: "Replace Magic Numbers with Named Constants",
            author: BOB,
            title: "Your code had magic numbers in the following locations:",
            type: "Magic Numbers"
            );

        public static IssueDetail LongClasses = new IssueDetail(
            id: 11,
            quote: "As with functions, smaller is the primary rule when it comes to designing classes.",
            author: BOB,
            title: "Long classes were detected:",
            type: "Long Classes"
        );

        public static IssueDetail EmptyCatches = new IssueDetail(
            id: 12,
            quote: "it's usually a very bad idea to have an empty catch block",
            author: BOB,
            title: "Empty catches were detected:",
            type: "Empty Catch Statements"
            );

        public static IssueDetail NestedConditions = new IssueDetail(
            id: 13,
            quote: "Do not nest your code beyond limits",
            author: BOB,
            title: "Nested if statements detected:",
            type: "Nested if Statements"
            );
        public static IssueDetail LongLines = new IssueDetail(
            id: 14,
            quote: "The code should be read vertically not horizontally. Therefore Long horizontal lines of code should be avoided.",
            author: BOB,
            title: "Long lines were detected:",
            type: "Long Horizontal Lines"
            );
        public static IssueDetail LongMethods = new IssueDetail(
            id: 15,
            quote: "Methods should be small, smaller than small.",
            author: BOB,
            title: "Long methods were detected:",
            type: "Long Method Bodies"
        );

        public static IssueDetail LineCount = new IssueDetail(
            id: 16,
            quote: "Keep it small",
            author: BOB,
            title: "Line count of the file",
            type: "File Line Count"
            );
        public static IssueDetail StaticNoParameters = new IssueDetail(
            id: 17,
            quote: "Static methods should have at least one parameter input",
            author: "",
            title: "You have static methods without parameters at :",
            type: "Static method parameter"
            );
        public static IssueDetail StaticState = new IssueDetail(
            id: 18,
            quote: "Static fields introduce global state and so should be avoided",
            author: "",
            title: "You have static fields and states at: ",
            type: "Static fields and global states"
            );
        public static IssueDetail RefOutParameter = new IssueDetail(
            id: 19,
            quote: "Method parameters must not use REF or OUT parameters; all results should be via a return",
            author: "",
            title: "You have methods with out or ref keyword at line: ",
            type: "Ref and Out keywords in func params"
            );
        public static IssueDetail ContainsAnd = new IssueDetail(
            id: 20,
            quote: "Method names that contain 'And' often indicate a method is doing more than one thing. Consider refacting into two methods.",
            author: "",
            title: "You have methods with 'And' in fucntion identifire at line: ",
            type: "Single responsibility"
            );

    }
}
