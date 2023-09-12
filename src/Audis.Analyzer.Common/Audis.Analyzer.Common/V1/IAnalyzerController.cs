using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Audis.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Audis.Analyzer.Common.V1
{
    /// <summary>
    /// Implement this interface, when the controller returns an analyzer result.
    /// </summary>
    public interface IAnalyzerController
    {
        Task<ActionResult<IEnumerable<AnalyzerResultDto>>> GetAnalyzerResult(
            [FromRoute] TenantId tenantId,
            [FromBody] AnalyzerRequestDto analyzerRequestDto,
            CancellationToken cancellationToken);
    }
}
