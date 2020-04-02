using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Audis.KnowledgeEnricher.Contract.V1
{
    /// <summary>
    /// Takes some knowledge and adds further knowledge based on existing knowledge.
    /// </summary>
    public interface IKnowledgeEnricher
    {
        /// <summary>
        /// Takes some knowledge and adds further knowledge based on the existing knowledge. Concrete implementations can inherit specific knowledge based on input knowledge.
        /// </summary>
        /// <param name="knowledge">The knowledge used as starting point for inheriting knowledge.</param>
        /// <returns>Returns the given knowledge plus potentially more inherited knowledge.</returns>
        Task<IReadOnlyCollection<KnowledgeDto>> EnrichKnowledge(Task<IReadOnlyCollection<KnowledgeDto>> knowledge);
    }
}
