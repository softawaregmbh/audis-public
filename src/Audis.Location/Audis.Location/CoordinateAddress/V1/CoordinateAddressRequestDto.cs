namespace Audis.Location.CoordinateAddress.V1
{
    public class CoordinateAddressRequestDto
    {
        public double Latitude { get; set; } = default!;
        public double Longitude { get; set; } = default!;
        public string LocationProvider { get; set; } = default!;
    }
}
