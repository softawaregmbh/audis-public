using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Request body for enriching/replacing question and answer texts
///     (e.g. after external translation). Provide either <see cref="QuestionId"/> or
///     <see cref="KnowledgeIdentifier"/> to identify the target question.
/// </summary>
public class EnrichQuestionRequestDto
{
    /// <summary>
    ///     Catalog question id in the form <c>catalog-name:lineNumber</c>.
    /// </summary>
    public QuestionId? QuestionId { get; set; }

    /// <summary>
    ///     Knowledge identifier of the question. Used when <see cref="QuestionId"/> is not provided.
    /// </summary>
    public KnowledgeIdentifier? KnowledgeIdentifier { get; set; }

    /// <summary>
    ///     Replacement question text. When null, the existing question text is left unchanged.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    ///     Answer texts to replace. Answers not listed are left unchanged.
    /// </summary>
    public IReadOnlyCollection<AnswerTextDto> Answers { get; set; } = Array.Empty<AnswerTextDto>();
}
