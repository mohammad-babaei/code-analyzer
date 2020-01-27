using RoslynAnalyzer.CleanCodeAnalyzers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynAnalyzer.CleanCodeAnalyzers
{
    class CodeDiagnosticResponse
    {
        public List<AnalyzeResult> results;

        public CodeDiagnosticResponse(List<AnalyzeResult> results)
        {
            this.results = results;
        }

        public static CodeDiagnosticResponse fromAnalyzers(List<BaseAnalyzer> analyzers)
        {
            return new CodeDiagnosticResponse(analyzers.Select(a => a.analyze()).ToList());
        }

        public override string ToString()
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return jsonString;
        }
    }
}
