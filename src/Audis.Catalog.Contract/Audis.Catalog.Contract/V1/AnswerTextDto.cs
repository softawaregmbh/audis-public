using Audis.Primitives;

namespace Audis.Catalog.Contract.V1;

/// <summary>
///     Answer text payload used when enriching catalog answer texts.
/// </summary>
public class AnswerTextDto
{
    required public AnswerId Id { get; set; }

    required public string Text { get; set; }
}
