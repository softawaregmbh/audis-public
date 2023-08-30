﻿using System.Threading;
using System.Threading.Tasks;
using Audis.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace Audis.Analyzer.Common.V1
{
    /// <summary>
    /// Implement this interface, when the controller returns an adaptive card.
    /// </summary>
    public interface IAdaptiveCardController
    {
        Task<ActionResult<AdaptiveCardResultDto>> GetAdaptiveCardResult(
            [FromRoute] TenantId tenantId,
            [FromBody] AnalyzerRequestDto analyzerRequestDto,
            CancellationToken cancellationToken);
    }
}
