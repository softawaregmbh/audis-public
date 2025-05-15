namespace Audis.Primitives;

/// <summary>
/// Base record for wrapping primitive types.
/// Used to prevent primitive obsession, see https://refactoring.guru/smells/primitive-obsession.
/// </summary>
/// <typeparam name="TValue">The type of the wrapped value.</typeparam>
public record Primitive<TValue>(TValue Value)
{
    public sealed override string? ToString() => this.Value?.ToString();
}