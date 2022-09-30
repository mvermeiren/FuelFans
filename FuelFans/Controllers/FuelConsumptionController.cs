using FuelFans.Clients;
using FuelFans.Converters;
using FuelFans.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuelFans.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelConsumptionController : ControllerBase
    {
        private readonly List<Car> cars = new List<Car> { };

        private const decimal PriceOfGasoline = 1.98m;

        private readonly IGeoCodeClient _geoCodeClient;

        public FuelConsumptionController(IGeoCodeClient geoCodeClient)
        {
            _geoCodeClient = geoCodeClient;
        }

        [HttpGet]
        [Route("[controller]/getbrandslist")]
        public IEnumerable<string> GetBrandsList()
        {
            return DataConverter.CarFuelConsumptions.Select(x => x.Brand).Distinct();
        }

        [HttpGet]
        [Route("[controller]/getmodelsbybrandlist")]
        public IEnumerable<string> GetModelsByBrandList(string brand)
        {
            return DataConverter.CarFuelConsumptions.Where(x => x.Brand.Equals(brand)).Select(y => y.Model);
        }

        [HttpPost]
        [Route("[controller]/calculate")]
        public async Task<CalculateOutput> Calculate([FromBody] CalculateInput input)
        {
            var originGeocoded = await _geoCodeClient.Geocode(input.Origin);
            var destinationGeocoded = await _geoCodeClient.Geocode(input.Destination);

            var calculatedRoute = await _geoCodeClient.CalculateRoute(originGeocoded.features[0].geometry.coordinates, destinationGeocoded.features[0].geometry.coordinates);

            var foundCar = DataConverter.CarFuelConsumptions.FirstOrDefault(x => x.Model == input.Model && x.Brand == input.Brand);
            if (foundCar == null)
                throw new Exception("Not found");

            var calculatedOutput = new CalculateOutput();
            calculatedOutput.Savings = new List<Saving>();
            calculatedOutput.Rewards = new List<Reward>();
            var baseline = new Saving
            {
                SpeedDelta = 120,
                FuelDelta = foundCar.HighwayConsumption / 100 * calculatedRoute.features[0].properties.distance,
                TimeDelta = TimeSpan.FromHours(calculatedRoute.features[0].properties.distance / 120)
            };
            calculatedOutput.Savings.Add(baseline);
            for (var i = -50; i <= 50; i += 10)
            {
                if (i == 0)
                    continue;
                var currentSpeed = 120 - i;

                var saving = new Saving
                {
                    SpeedDelta = currentSpeed,
                    FuelDelta = (foundCar.HighwayConsumption / 120) * i / 100 * calculatedRoute.features[0].properties.distance,
                    TimeDelta = TimeSpan.FromHours(baseline.TimeDelta.TotalHours / 120 * i),
                    NumberOfBeersSaved = (((foundCar.HighwayConsumption / 120) * i / 100 * calculatedRoute.features[0].properties.distance) - baseline.FuelDelta) * PriceOfGasoline
                };
                calculatedOutput.Savings.Add(saving);                
            }

            return calculatedOutput;
        }
    }
}