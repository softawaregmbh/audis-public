using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Catalog question as exposed by the Audis Catalog API, including all answer options
///     and synonyms. Returned by <c>POST .../Question/V1</c> for both <c>QuestionId</c> and
///     <c>KnowledgeIdentifier</c> lookups — this full payload is the training/export surface
///     for AI systems.
/// </summary>
public class QuestionDto
{
    required public QuestionId Id { get; set; }

    /// <summary>
    ///     Primary knowledge identifier of the question, if configured.
    /// </summary>
    public KnowledgeIdentifier? KnowledgeIdentifier { get; set; }

    /// <summary>
    ///     Question text as stored in the catalog (may contain markup).
    /// </summary>
    required public string Text { get; set; }

    /// <summary>
    ///     Question text with markup removed.
    /// </summary>
    required public string RawText { get; set; }

    /// <summary>
    ///     Visualization key from the catalog (e.g. <c>buttons</c>, <c>apisearch</c>).
    /// </summary>
    public string? Visualization { get; set; }

    /// <summary>
    ///     Selection behaviour: <c>SingleSelection</c> or <c>MultiSelection</c>.
    /// </summary>
    public string? SelectionType { get; set; }

    /// <summary>
    ///     Optional interrogation id echoed for correlation across parallel multilingual
    ///     interrogations when supplied on the request.
    /// </summary>
    public Guid? InterrogationId { get; set; }

    /// <summary>
    ///     All answer options including synonyms — required for AI training/export.
    /// </summary>
    required public IReadOnlyCollection<AnswerDto> Answers { get; set; } = Array.Empty<AnswerDto>();
}
