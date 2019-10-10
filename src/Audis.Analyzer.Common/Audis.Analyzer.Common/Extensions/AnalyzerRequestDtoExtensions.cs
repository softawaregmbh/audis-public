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
            var knowledge = analyzerRequestDto.Knowledge.FirstOrDefault(k => k.KnowledgeIdentifier == knowledgeIdentifier);

            if (knowledge == null)
            {
                value = null;
                return false;
            }

            var knowledgeValue = knowledge.Values.SingleOrDefault()?.KnowledgeValue;

            if (knowledgeValue == null)
            {
                value = null;
                return false;
            }

            value = knowledgeValue;
            return true;
        }

        public static bool IsAlreadyHandled(this AnalyzerRequestDto analyzerRequestDto, string key, string value)
        {
            return analyzerRequestDto.Data != null && (value == analyzerRequestDto.Data.Value<string>(key));
        }
    }
}