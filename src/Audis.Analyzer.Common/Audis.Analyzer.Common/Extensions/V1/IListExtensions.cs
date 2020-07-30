using System;
using System.Collections.Generic;
using Audis.Analyzer.Contract.V1;
using Audis.Primitives;

namespace Audis.Analyzer.Common.Extensions.V1
{
    public static class IListExtensions
    {
        public static void Add(
            this IList<SuggestedKnowledgeDto> suggestedKnowledges,
            string identifier,
            string value,
            double probability = 1.0,
            DateTime? lastUpdated = null,
            KnowledgeOrigin origin = KnowledgeOrigin.Client)
        {
            if (identifier is null)
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            if (value != null)
            {
                suggestedKnowledges.Add(new SuggestedKnowledgeDto
                {
                    KnowledgeIdentifier = KnowledgeIdentifier.From(identifier),
                    Values = new HashSet<KnowledgeValueDto>(new KnowledgeValueDto[]
                    {
                        new KnowledgeValueDto
                        {
                            KnowledgeValue = KnowledgeValue.From(value)
                        }
                    }),
                    Probability = probability,
                    LastUpdated = lastUpdated ?? DateTime.UtcNow,
                    Origin = origin
                });
            }
        }
    }
}
