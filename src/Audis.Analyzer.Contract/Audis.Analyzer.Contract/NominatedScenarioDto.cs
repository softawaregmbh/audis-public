using System;
using System.Collections.Generic;
using System.Text;
using Audis.Primitives;

namespace Audis.Analyzer.Contract
{
    public class NominatedScenarioDto
    {
        public ScenarioIdentifier ScenarioIdentifier { get; set; }
        public int NominatedAtProcessStepId { get; set; }
    }
}
