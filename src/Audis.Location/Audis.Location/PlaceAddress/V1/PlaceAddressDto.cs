namespace Audis.Location.PlaceAddress.V1
{
    public class PlaceAddressDto
    {
        public string? PlaceId { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }

        public string? ObjectName { get; set; }
    }
}
