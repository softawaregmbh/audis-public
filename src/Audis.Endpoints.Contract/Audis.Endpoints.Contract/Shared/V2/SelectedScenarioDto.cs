namespace Audis.Endpoints.Contract.Shared.V2;

/// <summary>
///     Dispatcher selection for one active domain. <see cref="Reason"/> is the override
///     justification for this domain only (null when the engine suggestion was kept).
/// </summary>
public class SelectedScenarioDto : ScenarioDto
{
    public string? Reason { get; set; }
}
