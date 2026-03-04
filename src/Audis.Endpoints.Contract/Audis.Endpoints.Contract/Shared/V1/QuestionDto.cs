using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1;

public class QuestionDto
{
    required public QuestionId Id { get; set; }

    required public string Text { get; set; }

    required public string Type { get; set; }

    public string? KnowledgeSummary { get; set; }
}
