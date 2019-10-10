using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Audis.Dtos
{
    public class InterrogationRequestDto
    {
        public Guid Id { get; set; }
        public int ProcessStepId { get; set; }
        public IEnumerable<KnowledgeDto> Knowledge { get; set; }
        public IEnumerable<NominatedScenarioDto> NominatedScenarios { get; set; }

        /// <summary>
        /// Free data object where implementation-specific data/identification/... can be stored.
        /// Adapters to concrete implementations may use this field for their specific data structure.
        /// </summary>
        public JObject Data { get; set; }
    }
}
