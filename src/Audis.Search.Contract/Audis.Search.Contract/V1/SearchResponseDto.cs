namespace Audis.Search.Contract.V1;

/// <summary>
///     Response body from an external answer-search endpoint.
///     Mirrors the Web <c>ApiSearchDto</c> shape (<c>Answers</c> + optional top-level <c>Reason</c>).
/// </summary>
public class SearchResponseDto
{
    /// <summary>
    ///     Ranked matches. Prefer returning <see cref="SearchResultDto.Id"/> so Audis can
    ///     map back to full catalog answers.
    /// </summary>
    required public IReadOnlyCollection<SearchResultDto> Answers { get; set; }

    /// <summary>
    ///     Optional global reason for the whole result set (e.g. preconfigured override).
    /// </summary>
    public string? Reason { get; set; }
}
