using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1
{
    public class AnswerDto
    {
        public AnswerId Id { get; set; } = default!;
        public string Text { get; set; } = default!;
    }
}
