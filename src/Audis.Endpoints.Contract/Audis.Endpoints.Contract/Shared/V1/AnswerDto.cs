using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1;

public class AnswerDto
{
    required public AnswerId Id { get; set; }

    required public string Text { get; set; }
}
