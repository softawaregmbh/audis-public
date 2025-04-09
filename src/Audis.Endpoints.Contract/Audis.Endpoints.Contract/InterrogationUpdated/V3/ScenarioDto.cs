using System.Collections.Generic;

namespace Audis.Endpoints.Contract.InterrogationUpdated.V3
{
    public class ScenarioDto
    {
        required public string Identifier { get; set; }

        required public string Name { get; set; }

        public IEnumerable<string> DispositionCodes { get; set; } = new List<string>();
    }
}
