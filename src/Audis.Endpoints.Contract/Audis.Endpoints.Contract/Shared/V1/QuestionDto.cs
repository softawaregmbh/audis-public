using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1
{
    public class QuestionDto
    {
        public QuestionId Id { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
