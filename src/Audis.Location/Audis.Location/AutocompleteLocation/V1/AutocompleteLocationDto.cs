using Audis.Location.Enums.V1;

namespace Audis.Location.AutocompleteLocation.V1
{
    public class AutocompleteLocationDto
    {
        public AutocompleteLocationDto(string location, string placeId, LocationProviderType locationProviderType)
        {
            this.Location = location;
            this.PlaceId = placeId;
            this.LocationProviderType = locationProviderType;
        }

        public string Location { get; set; }
        public string PlaceId { get; set; }
        public string LocationProvider { get; set; }
        public LocationProviderType LocationProviderType { get; set; }
    }
}
  