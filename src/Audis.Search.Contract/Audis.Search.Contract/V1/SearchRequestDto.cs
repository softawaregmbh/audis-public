using Audis.Primitives;

namespace Audis.Search.Contract.V1;

/// <summary>
///     Request body Audis sends to an external answer-search endpoint.
///     Provides the search term and the candidate answer pool (with synonyms) to rank against.
/// </summary>
public class SearchRequestDto
{
    public TenantId? TenantId { get; set; }

    public RevisionId? RevisionId { get; set; }

    /// <summary>
    ///     Catalog question id in the form <c>catalog-name:lineNumber</c>.
    /// </summary>
    public QuestionId? QuestionId { get; set; }

    /// <summary>
    ///     Knowledge identifier of the question being searched (e.g. <c>#symptom</c>).
    /// </summary>
    public KnowledgeIdentifier? KnowledgeIdentifier { get; set; }

    /// <summary>
    ///     Free-text term entered by the caller (maps to the existing API <c>searchTerm</c>).
    /// </summary>
    required public string SearchTerm { get; set; }

    /// <summary>
    ///     Candidate answers to search in. Aligns with the in-process search item pool
    ///     (text, optional context, synonyms) and with catalog <c>AnswerDto</c> ids/text.
    /// </summary>
    required public IReadOnlyCollection<SearchCandidateDto> Answers { get; set; }

    /// <summary>
    ///     Maximum number of ranked matches to return. When omitted, the endpoint may apply its default.
    /// </summary>
    public int? Take { get; set; }
}
