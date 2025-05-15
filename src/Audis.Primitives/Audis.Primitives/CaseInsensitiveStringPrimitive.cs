using System;

namespace Audis.Primitives;

/// <summary>
/// A <see cref="Primitive{TValue}" /> where the underlying type is a <see cref="string" />.
/// The comparision with other strings is case-insensitive.
/// Values must not be <see langword="null" />.
/// </summary>
public record CaseInsensitiveStringPrimitive
    : Primitive<string>
{
    public CaseInsensitiveStringPrimitive(string value)
        : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException("Value must not be null or whitespace.");
        }
    }

    public virtual bool Equals(CaseInsensitiveStringPrimitive? other) => other is not null &&
                                                                         this.Value.Equals(other.Value,
                                                                             StringComparison
                                                                                 .InvariantCultureIgnoreCase);

    public override int GetHashCode() => this.Value.ToLowerInvariant().GetHashCode();
}