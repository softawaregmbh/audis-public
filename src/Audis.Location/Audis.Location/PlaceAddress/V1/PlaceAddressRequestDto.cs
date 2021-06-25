namespace Audis.Location.PlaceAddress.V1
{
    public class PlaceAddressRequestDto
    {
        public string PlaceId { get; set; }
        public string LocationProvider { get; set; } = default!;
    }
}
