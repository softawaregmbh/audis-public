using System;
using System.Collections.Generic;
using Audis.Primitives;

namespace Audis.KnowledgeEnrichers.Contract.V1
{
    public class KnowledgeDto
    {
        public KnowledgeIdentifier KnowledgeIdentifier { get; set; } = default!;

        public HashSet<KnowledgeValueDto> Values { get; set; } = new HashSet<KnowledgeValueDto>();

        public KnowledgeOrigin Origin { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
