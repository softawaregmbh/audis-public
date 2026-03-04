using Audis.Primitives;

namespace Audis.Endpoints.Contract.Shared.V1;

public class KnowledgeValueDto
{
    public KnowledgeValue KnowledgeValue { get; set; } = default!;
    public AnswerId? AnswerId { get; set; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return this.Equals((KnowledgeValueDto)obj);
    }

    public bool Equals(KnowledgeValueDto other) => this.KnowledgeValue == other.KnowledgeValue;

    public override int GetHashCode() => this.KnowledgeValue?.GetHashCode() ?? 0;
}
