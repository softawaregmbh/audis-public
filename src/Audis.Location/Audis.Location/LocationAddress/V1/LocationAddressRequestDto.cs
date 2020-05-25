using System;
using System.Collections.Generic;
using System.Text;

namespace Audis.Location.LocationAddress.V1
{
    public class LocationAddressRequestDto
    {
        /// <summary>
        /// Either Address, Placeid or both can be set in the analyser.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Either Address, Placeid or both can be set in the analyser.
        /// </summary>
        public string? PlaceId { get; set; }
        public string LocationProvider { get; set; }
    }
}
