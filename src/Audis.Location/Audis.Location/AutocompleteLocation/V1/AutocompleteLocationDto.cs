namespace Audis.Location.AutocompleteLocation.V1
{
    public class AutocompleteLocationDto
    {
        public AutocompleteLocationDto(string location, string placeId, string locationProvider)
        {
            this.Location = location;
            this.PlaceId = placeId;
            this.LocationProvider = locationProvider;
        }

        public string Location { get; set; } = default!;
        public string PlaceId { get; set; } = default!;
        public string LocationProvider { get; set; } = default!;
    }
}
  