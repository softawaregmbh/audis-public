using Audis.Primitives;

namespace Audis.Search.Contract.V1;

/// <summary>
///     One ranked match from an external search endpoint.
///     Aligns with in-process search results (<c>Probability</c>, <c>Reason</c>, <c>Text</c>)
///     and with Web <c>ApiSearchAnswerDto</c> (<c>Id</c>, <c>Reason</c>).
/// </summary>
public class SearchResultDto
{
    /// <summary>
    ///     Matched answer id. Preferred correlation key back to the request candidates.
    /// </summary>
    public AnswerId? Id { get; set; }

    /// <summary>
    ///     Matched answer text. Optional when <see cref="Id"/> is set.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    ///     Rank score / probability (higher is better). Optional when the endpoint only returns order.
    /// </summary>
    public double? Probability { get; set; }

    /// <summary>
    ///     Why this answer was returned as a match.
    /// </summary>
    public string? Reason { get; set; }
}
