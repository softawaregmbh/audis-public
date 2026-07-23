namespace Audis.ContextSearch.Contract;

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
public record SearchResult<T>(string Text, double? Probability, string? Reason = default, T? Result = default);

public record SearchResult(string Text, double? Probability)
    : SearchResult<object>(Text, Probability);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
