using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Audis.Primitives;

namespace Audis.Analyzer.Common.Extensions
{
    public static class AnalyzerRequestDtoExtensions
    {
        public static bool TryGetSingleKnowledgeValue(this AnalyzerRequestDto analyzerRequestDto, KnowledgeIdentifier knowledgeIdentifier, out KnowledgeValue value)
        {
            if (TryGetKnowledgeValues(analyzerRequestDto, knowledgeIdentifier, out var values))
            {
                value = values.SingleOrDefault();
                return true;
            }

            value = null;
            return false;
        }

        public static bool TryGetKnowledgeValues(this AnalyzerRequestDto analyzerRequestDto, KnowledgeIdentifier knowledgeIdentifier, out IEnumerable<KnowledgeValue> values)
        {
            var knowledge = analyzerRequestDto.Knowledge.FirstOrDefault(k => k.KnowledgeIdentifier == knowledgeIdentifier);

            if (knowledge == null)
            {
                values = null;
                return false;
            }

            var knowledgeValues = knowledge.Values.Select(v => v.KnowledgeValue);

            if (knowledgeValues == null || !knowledgeValues.Any())
            {
                values = null;
                return false;
            }

            values = knowledgeValues;
            return true;
        }

        public static bool IsAlreadyHandled(this AnalyzerRequestDto analyzerRequestDto, string key, string value)
        {
            return analyzerRequestDto.Data != null && (value == analyzerRequestDto.Data.Value<string>(key));
        }
    }
}