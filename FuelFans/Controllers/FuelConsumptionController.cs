using FuelFans.Models;
using Microsoft.AspNetCore.Mvc;

namespace FuelFans.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelConsumptionController : ControllerBase
    {
        private readonly List<Car> cars = new List<Car> {};

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
        public CalculateOutput Calculate([FromBody]CalculateInput input)
        {

            return new CalculateOutput();
        }
    }
}