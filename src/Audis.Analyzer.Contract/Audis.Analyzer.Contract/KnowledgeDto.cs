using System;
using System.Collections.Generic;
using Audis.Primitives;

namespace Audis.Analyzer.Contract
{
    public class KnowledgeDto
    {
        public KnowledgeIdentifier KnowledgeIdentifier { get; set; }

        public HashSet<KnowledgeValueDto> Values { get; set; } = new HashSet<KnowledgeValueDto>();

        public KnowledgeOrigin Origin { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
