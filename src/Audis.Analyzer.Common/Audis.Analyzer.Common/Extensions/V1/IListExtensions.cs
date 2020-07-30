using System;
using System.Collections.Generic;
using System.Linq;
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
            KnowledgeOrigin origin = KnowledgeOrigin.Client)
        {
            if (identifier is null)
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                var knowledgeIdent = KnowledgeIdentifier.From(identifier);
                var knowledgeValue = KnowledgeValue.From(value);

                var existingKnowledge = suggestedKnowledges.FirstOrDefault(k => k.KnowledgeIdentifier == knowledgeIdent);
                if (existingKnowledge == null)
                {
                    suggestedKnowledges.Add(new SuggestedKnowledgeDto
                    {
                        KnowledgeIdentifier = knowledgeIdent,
                        Values = new HashSet<KnowledgeValueDto>(new KnowledgeValueDto[]
                        {
                        new KnowledgeValueDto
                        {
                            KnowledgeValue = knowledgeValue
                        }
                        }),
                        Probability = probability,
                        LastUpdated = DateTime.UtcNow,
                        Origin = origin
                    });
                }
                else
                {
                    existingKnowledge.Values.Add(new KnowledgeValueDto
                    {
                        KnowledgeValue = knowledgeValue
                    });

                    existingKnowledge.LastUpdated = DateTime.UtcNow;
                    existingKnowledge.Probability = (existingKnowledge.Probability + probability) / 2;
                    existingKnowledge.Origin = origin;
                }
            }
        }
    }
}
