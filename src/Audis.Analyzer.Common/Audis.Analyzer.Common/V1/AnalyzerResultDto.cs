using System;
using System.Collections.Generic;
using System.Text;
using Audis.Analyzer.Contract.V1;
using Newtonsoft.Json.Linq;

namespace Audis.Analyzer.Common.V1
{
    public class AnalyzerResultDto
    {
        public Guid InterrogationId { get; set; }
        public int InterrogationProcessStepId { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Optional knowledges, which can be suggested by the analyzer.
        /// Each suggested knowledge has a <see cref="SuggestedKnowledgeDto.Probability"/>.
        /// </summary>
        public IEnumerable<SuggestedKnowledgeDto> SuggestedKnowledges { get; set; }

        /// <summary>
        /// Unstructured data object which can be used for information exchange between multiple process steps for a specific analyzer.
        /// </summary>
        public JObject Data { get; set; }
    }
}
