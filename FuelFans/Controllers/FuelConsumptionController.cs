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

            var calculatedRoute = await _geoCodeClient.CalculateRoute(originGeocoded.features[0].geometry.coordinates, originGeocoded.features[0].geometry.coordinates);

            //calculatedRoute.features[0].properties.distance
            //calculatedRoute.features[0].properties.time

            var foundCar = DataConverter.CarFuelConsumptions.FirstOrDefault(x => x.Model == input.Model && x.Brand == input.Brand);
            if (foundCar == null)
                throw new Exception("Not found");

            var calculatedOutput = new CalculateOutput();
            for (var i = 70; i <= 170; i += 10)
            {
                var averageSpeed = calculatedRoute.features[0].properties.distance / calculatedRoute.features[0].properties.time;
                calculatedOutput.Savings.Add(
                    new Saving
                    {
                        SpeedDelta = i,
                        FuelDelta = (foundCar.HighwayConsumption / 120) * i / 100 * calculatedRoute.features[0].properties.distance,
                        //TimeDelta = averageSpeed / calculatedRoute.features[0].properties.distance -
                    });
            }

            return calculatedOutput;
        }
    }
}