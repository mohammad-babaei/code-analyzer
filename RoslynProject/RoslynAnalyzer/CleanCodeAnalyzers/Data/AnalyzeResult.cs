using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynAnalyzer.CleanCodeAnalyzers.Data
{
    class AnalyzeResult
    {
        public IssueDetail issueDetail;
        public List<Warning> warnings;

        public AnalyzeResult(IssueDetail issueDetail, List<Warning> warnings)
        {
            this.issueDetail = issueDetail;
            this.warnings = warnings;
        }

        public override string ToString()
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return jsonString;
        }
    }
}
