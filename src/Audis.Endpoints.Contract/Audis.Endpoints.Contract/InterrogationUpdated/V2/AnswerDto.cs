using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V2
{
    public class AnswerDto
    {
        public AnswerId Id { get; set; } = default!;
        public string Text { get; set; } = default!;
    }
}
