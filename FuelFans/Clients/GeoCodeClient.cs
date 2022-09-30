using Flurl;
using Flurl.Http;
using FuelFans.Models;

namespace FuelFans.Clients
{
    public class GeoCodeClient : IGeoCodeClient
    {

        public async Task<GeoCodeResult> Geocode(string address)
        {
            return await $"https://api.geoapify.com/v1/geocode/search?text={Url.Encode(address)}&apiKey=70ec52f673b44f309a777254e6e3ffc3"
                .GetJsonAsync<GeoCodeResult>();
        }
        public async Task<RouteResult> CalculateRoute(float[] originCoordinates, float[] destinationCoordinates)
        {
            var joinedOrigin = string.Join(",", originCoordinates.Reverse().Select(x => x.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
            var joinedDestination = string.Join(",", destinationCoordinates.Reverse().Select(x => x.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
            return await $"https://api.geoapify.com/v1/routing?waypoints={Url.Encode(joinedOrigin)}|{Url.Encode(joinedDestination)}&mode=drive&apiKey=70ec52f673b44f309a777254e6e3ffc3"
                .GetJsonAsync<RouteResult>();
        }
    }
}
