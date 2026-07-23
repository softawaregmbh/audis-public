namespace Audis.ContextSearch.Contract;

/// <summary>
///     Preconfigured search mapping used by <see cref="Logics.IPreconfiguredSearchLogic{T}"/>.
/// </summary>
public class PreconfiguredApiSearchSetting
{
    required public string SearchText { get; set; }

    required public string[] ExpectedResultTexts { get; set; } = Array.Empty<string>();

    required public bool OverrideAutosearchResults { get; set; } = false;
}
