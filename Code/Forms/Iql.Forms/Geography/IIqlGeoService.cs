using System.Threading.Tasks;

namespace Iql.Forms.Geography
{
    public interface IIqlGeoService
    {
        Task<bool> EnableLocationRequestAsync();
        Task<bool> CanRequestLocationAsync();
        Task<bool> IsEnabledAsync();
        Task<IqlGeographicCoordinates> DefaultCoordinatesAsync();
        Task<IqlCurrentLocation> GetCurrentLocationAsync(IqlGetLocationSettings settings = null);
    }
}