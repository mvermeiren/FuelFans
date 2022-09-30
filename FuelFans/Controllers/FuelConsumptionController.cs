using FuelFans.Clients;
using FuelFans.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuelFans.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelConsumptionController : ControllerBase
    {
        private readonly List<Car> cars = new List<Car> { };

        private readonly IGeoCodeClient _geoCodeClient;

        public FuelConsumptionController(IGeoCodeClient geoCodeClient)
        {
            _geoCodeClient = geoCodeClient;
        }

        public FuelConsumptionController()
        {
            cars = new List<Car> { };
            cars.Add(new Car("bmw", "320", "petrol"));
            cars.Add(new Car("bmw", "330", "diesel"));
            cars.Add(new Car("ford", "focus", "diesel"));
            cars.Add(new Car("ford", "f150", "petrol"));
            cars.Add(new Car("polestar", "2", "electric"));
            cars.Add(new Car("polestar", "3", "electric"));
        }

        [HttpGet]
        [Route("[controller]/getbrandslist")]
        public IEnumerable<string> GetBrandsList()
        {
            return cars.Select(x => x.Brand).Distinct();
        }

        [HttpGet]
        [Route("[controller]/getmodelsbybrandlist")]
        public IEnumerable<string> GetModelsByBrandList(string brand)
        {
            return cars.Where(x => x.Brand.Equals(brand)).Select(y => y.Model);
        }

        [HttpPost]
        [Route("[controller]/calculate")]
        public async Task<CalculateOutput> Calculate([FromBody] CalculateInput input)
        {
            var originGeocoded = await _geoCodeClient.Geocode(input.Origin);
            var destinationGeocoded = await _geoCodeClient.Geocode(input.Destination);

            var calculatedRoute = await _geoCodeClient.CalculateRoute(originGeocoded.features[0].geometry.coordinates, originGeocoded.features[0].geometry.coordinates);

            //calculatedRoute.features[0].properties.distance
            //calculatedRoute.features[0].properties.time

            var calculatedOutput = new CalculateOutput();
            for (var i = -50; i < 50; i += 10)
            {
                calculatedOutput.Savings.Add(
                        new Saving
                        {
                            SpeedDelta = i,
                            FuelDelta = 0.5 * i / 10,
                            TimeDelta = TimeSpan.FromMinutes(2 * i / 10)
                        });
            }

            return calculatedOutput;
        }
    }
}