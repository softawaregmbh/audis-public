using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Request body for retrieving a catalog question including its answer options
///     and synonyms. This is the training/export surface: a successful response is a
///     full <see cref="QuestionDto"/> with all answers and synonyms populated.
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
    ///     Used when <see cref="QuestionId"/> is not provided. Looking up by knowledge
    ///     identifier returns the same full question payload (answers + synonyms) as by id.
    /// </summary>
    public KnowledgeIdentifier? KnowledgeIdentifier { get; set; }

    /// <summary>
    ///     Optional interrogation id for correlation across parallel multilingual interrogations.
    ///     Catalog export for training may omit this; live translation should send it.
    /// </summary>
    public Guid? InterrogationId { get; set; }
}
