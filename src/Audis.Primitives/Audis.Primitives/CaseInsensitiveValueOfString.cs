using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Primitives
{
    /// <summary>
    /// A <see cref="ValueOf{TValue, TThis}"/> where the underlying type is a <see cref="string"/>.
    /// The comparision with other strings is case-insensitive.
    /// Values must not be null.
    /// </summary>
    public class CaseInsensitiveValueOfString<TThis> : ValueOf<string, TThis>
        where TThis : ValueOf<string, TThis>, new()
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Value))
            {
                throw new ArgumentNullException(nameof(this.Value));
            }
        }

        protected override bool Equals(ValueOf<string, TThis> other)
        {
            return EqualityComparer<string>.Default.Equals(this.Value.ToLowerInvariant(), other.Value.ToLowerInvariant());
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(this.Value.ToLowerInvariant());
        }
    }
}
