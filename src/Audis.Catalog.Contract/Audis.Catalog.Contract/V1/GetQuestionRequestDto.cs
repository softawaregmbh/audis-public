using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Request body for retrieving a catalog question including its answer options.
///     Provide either <see cref="QuestionId"/> or <see cref="KnowledgeIdentifier"/> (or both;
///     <see cref="QuestionId"/> takes precedence when both are set).
/// </summary>
public class GetQuestionRequestDto
{
    /// <summary>
    ///     Catalog question id in the form <c>catalog-name:lineNumber</c>.
    /// </summary>
    public QuestionId? QuestionId { get; set; }

    /// <summary>
    ///     Knowledge identifier of the question (e.g. <c>#age</c>).
    ///     Used when <see cref="QuestionId"/> is not provided.
    /// </summary>
    public KnowledgeIdentifier? KnowledgeIdentifier { get; set; }
}
