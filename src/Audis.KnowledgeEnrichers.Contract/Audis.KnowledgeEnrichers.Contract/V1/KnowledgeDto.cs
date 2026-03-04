using System;
using System.Collections.Generic;
using Audis.Contract.V1;
using Audis.Primitives;

namespace Audis.KnowledgeEnrichers.Contract.V1
{
    public class KnowledgeDto
    {
        public KnowledgeIdentifier KnowledgeIdentifier { get; set; } = default!;

        public HashSet<KnowledgeValueDto> Values { get; set; } = new HashSet<KnowledgeValueDto>();

        public KnowledgeOrigin Origin { get; set; }

        public DateTime? LastUpdated { get; set; }
        
        public KnowledgeMetadata? Metadata { get; set; }
    }
}
