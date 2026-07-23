using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Catalog question as exposed by the Audis Catalog API, including answer options.
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

    required public IReadOnlyCollection<AnswerDto> Answers { get; set; } = Array.Empty<AnswerDto>();
}
