using FuelFans.Models;

namespace FuelFans.Clients
{
    public interface IGeoCodeClient
    {
        Task<GeoCodeResult> Geocode(string address);

        Task<RouteResult> CalculateRoute(float[] originCoordinates, float[] destinationCoordinates);
     }
}
