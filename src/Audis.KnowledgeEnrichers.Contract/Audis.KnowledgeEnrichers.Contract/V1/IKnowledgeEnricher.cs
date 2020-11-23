using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Audis.KnowledgeEnrichers.Contract.V1
{
    /// <summary>
    /// Takes some knowledge and returns inherited knowledge.
    /// </summary>
    public interface IKnowledgeEnricher
    {
        /// <summary>
        /// Takes some knowledge and returns only the inherited knowlege if the knowledge enricher has anything to add for the given knowledge.
        /// </summary>
        /// <param name="knowledge">The knowledge used as starting point for inheriting knowledge.</param>
        /// <param name="isStartOfInterrogation"><see langword="True"/> if the enricher is called when starting the interrogation.</param>
        /// <param name="configuration">Optional configuration for the knowledge enricher.</param>
        /// <returns>Returns the inherited knowledge or an empty list.</returns>
        Task<IReadOnlyCollection<KnowledgeDto>> EnrichKnowledgeAsync(
            IReadOnlyCollection<KnowledgeDto> knowledge,
            bool isStartOfInterrogation,
            JObject? configuration = null);
    }
}
