using System;
using System.Collections.Generic;
using Audis.Primitives;

namespace Audis.Endpoints.Contract.InterrogationCompleted.V1
{
    public class InterrogationCompletedDto
    {
        public Guid Id { get; set; }
        public int FinalProcessStepId { get; set; }
        public string FreeText { get; set; }
        public IReadOnlyCollection<ScenarioIdentifier> SelectedScenarios { get; set; }
    }
}
