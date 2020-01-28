using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoCompiler2.Data
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
        
        public static IssueDetail meaningfulNames = new IssueDetail(
            id: 1,
            quote: "The name of a variable, function, or class, should answer all the big quetions. It should tell you why it exists, what it does, and how it is used.",
            author: BOB,
            title: "Non-English identifiers found:",
            type: "Meaningful Names"
            );

        public static IssueDetail excessiveParameters = new IssueDetail(
            id: 2, 
            quote: "Functions with many parameters are hard to read and hard to use.",
            author: "Cunningham & Cunningham, Code Smells",
            title: "Methods having more than 3 parameters",
            type: "Excessive number of methods parameters"
            );
    }
}
