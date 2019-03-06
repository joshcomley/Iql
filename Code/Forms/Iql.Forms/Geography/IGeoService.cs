using System.Threading.Tasks;

namespace Iql.Forms.Geography
{
    public interface IIqlGeoService
    {
        Task<bool> EnableLocationRequestAsync();
        Task<bool> CanRequestLocationAsync();
        Task<bool> IsEnabledAsync();
        Task<GeographicCoordinates> DefaultCoordinatesAsync();
        Task<CurrentLocation> GetCurrentLocationAsync(GetLocationSettings settings = null);
    }
}