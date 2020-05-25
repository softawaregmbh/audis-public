using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Location.LocationAddress.V1
{
    public class LocationAddressRequestDto
    {
        /// <summary>
        /// Either <see cref="Address"/>, <see cref="PlaceId"/> or both can be set in the analyzer.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Either <see cref="Address"/>, <see cref="PlaceId"/> or both can be set in the analyzer.
        /// </summary>
        public string? PlaceId { get; set; }

        public string LocationProvider { get; set; } = default!;
    }
}
