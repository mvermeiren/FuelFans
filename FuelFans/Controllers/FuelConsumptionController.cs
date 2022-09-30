using Microsoft.AspNetCore.Mvc;

namespace FuelFans.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelConsumptionController : ControllerBase
    {
        private static readonly string[] brands = new[]{"bmw", "ford", "polestar"};
        private static readonly Dictionary<string, string> models = new Dictionary<string, string> { 
            { "bmw", "320" },
            { "bmw", "330" },
            { "ford", "focus" },
            { "ford", "f150" },
            { "polestar", "2" },
            { "polestar", "3" },

        };

        public FuelConsumptionController()
        {
        }

        [HttpGet]
        [Route("[controller]/getbrandslist")]
        public IEnumerable<string> GetBrandsList()
        {
            return brands;
        }

        [HttpGet]
        [Route("[controller]/getmodelsbybrandlist")]
        public IEnumerable<string> GetModelsByBrandList(string brand)
        {
            return models.Where(x => x.Key.Equals(brand)).Select(y => y.Value);
        }
    }
}