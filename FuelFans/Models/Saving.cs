namespace FuelFans.Models
{
    public class Saving
    {
        public int SpeedDelta { get; set; }
        public decimal FuelDelta { get; set; }
        public TimeSpan TimeDelta { get; set; }
        public decimal NumberOfBeersSaved { get; set; }
    }
}
