using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Analyzer.Contract.V1
{
    public class SuggestedKnowledgeDto : KnowledgeDto
    {
        /// <summary>
        /// Probability for the suggested knowledge, value between 0..1.
        /// </summary>
        public double Probability { get; set; }
    }
}
