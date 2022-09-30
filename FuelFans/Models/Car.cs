namespace FuelFans.Models
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string FuelType { get; set; }

        public Car(string brand, string model, string fuelType)
        {
            Brand = brand;
            Model = model;
            FuelType = fuelType;
        }
    }
}
