﻿namespace Audis.Location.CoordinateAddress.V1
{
    public class CoordinateAddressRequestDto
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string LocationProvider { get; set; } = default!;
    }
}
