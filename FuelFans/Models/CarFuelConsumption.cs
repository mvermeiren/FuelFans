namespace FuelFans.Models
{
    public class CarFuelConsumption
    {
        public string Brand { get; set; } = default!;

        public string Model { get; set; } = default!;

        public decimal CityConsumption { get; set; }

        public decimal HighwayConsumption { get; set; }

        public string FuelType { get; set; } = default!;
    }
}
