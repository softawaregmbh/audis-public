using System.Collections.Generic;
using Audis.Analyzer.Contract.V1;
using Audis.Primitives;

namespace Audis.Analyzer.Contract.Extensions.V1
{
    public static class KnowledgeDtoExtensions
    {
        public static SuggestedKnowledgeDto From(string knowledgeIdentifier, string value)
        {
            return new SuggestedKnowledgeDto()
            {
                KnowledgeIdentifier = new KnowledgeIdentifier(knowledgeIdentifier),
                Values = new HashSet<KnowledgeValueDto>()
                {
                    new KnowledgeValueDto()
                    {
                        KnowledgeValue = new KnowledgeValue(value)
                    }
                }
            };
        }
    }
}
