namespace Audis.Location.CoordinateAddress.V1
{
    public class CoordinateAddressDto
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }

        public string? ObjectName { get; set; }
    }
}
