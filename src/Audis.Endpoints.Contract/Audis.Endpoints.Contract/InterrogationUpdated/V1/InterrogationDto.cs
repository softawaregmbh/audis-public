using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V1
{
    public class InterrogationDto
    {
        public Guid Id { get; set; }
        public int ProcessStepId { get; set; }
        public IEnumerable<KnowledgeDto> Knowledge { get; set; }
        public IEnumerable<NominatedScenarioDto> NominatedScenarios { get; set; }

        /// <summary>
        /// Free data object where implementation-specific data/identification/... can be stored.
        /// </summary>
        public JObject Data { get; set; }
    }
}
