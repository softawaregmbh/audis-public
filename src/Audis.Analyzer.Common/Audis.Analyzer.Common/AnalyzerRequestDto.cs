﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audis.Dtos;
using Audis.Primitives;
using Newtonsoft.Json.Linq;

namespace Audis.Analyzer.Common
{
    public class AnalyzerRequestDto
    {
        public class Question
        {
            public QuestionId Id { get; set; }
            public IEnumerable<Answer> Answers { get; set; }
        }

        public class Answer
        {
            public KnowledgeIdentifier KnowledgeIdentifier { get; set; }
            public KnowledgeValue knowledgeValue { get; set; }
        }

        public Guid InterrogationId { get; set; }
        public int InterrogationProcessStepId { get; set; }
        public Question CurrentQuestion { get; set; }
        public IEnumerable<KnowledgeDto> Knowledge { get; set; }

        /// <summary>
        /// Unstructured data object which can be used for information exchange between multiple process steps for a specific analyzer.
        /// </summary>
        public JObject Data { get; set; }
    }
}
