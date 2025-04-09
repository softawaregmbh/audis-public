using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V3
{
    public class AnswerDto
    {
        required public AnswerId Id { get; set; }

        required public string Text { get; set; }
    }
}
