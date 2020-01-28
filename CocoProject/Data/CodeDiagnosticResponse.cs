using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocoCompiler2.Data;

namespace CocoCompiler2.Data
{
    class CodeDiagnosticResponse
    {
        public List<AnalyzeResult> results;

        public CodeDiagnosticResponse(List<AnalyzeResult> results)
        {
            this.results = results;
        }

        public override string ToString()
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return jsonString;
        }
    }
}
