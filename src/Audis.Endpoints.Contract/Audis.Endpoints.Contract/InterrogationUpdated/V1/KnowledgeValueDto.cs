using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V1
{
    public class KnowledgeValueDto
    {
        public KnowledgeValue KnowledgeValue { get; set; } = default!;
        public AnswerId AnswerId { get; set; } = default!;

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

        public bool Equals(KnowledgeValueDto other)
        {
            return this.KnowledgeValue == other.KnowledgeValue;
        }

        public override int GetHashCode()
        {
            return this.KnowledgeValue?.GetHashCode() ?? 0;
        }
    }
}
