using System;
using System.Collections.Generic;
using System.Text;
using Audis.Primitives;

namespace Audis.Dtos
{
    public class NominatedScenarioDto
    {
        public ScenarioIdentifier ScenarioIdentifier { get; set; }
        public int NominatedAtProcessStepId { get; set; }
    }
}
