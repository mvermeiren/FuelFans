using FuelFans.Models;
using OfficeOpenXml;

#pragma warning disable CS8601 // Possible null reference assignment.
namespace FuelFans.Converters
{
    public static class DataConverter
    {
        public static ICollection<CarFuelConsumption> CarFuelConsumptions = new List<CarFuelConsumption>();

        public static void ReadData()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var excelPackage = new ExcelPackage(new FileInfo("Data/FuelConsumptionData.xlsx"));

            var worksheet = excelPackage.Workbook.Worksheets["21"];
            var numberOfRows = worksheet.Dimension.End.Row; 

            for (int i = 2; i <= numberOfRows; i++)
            {
                var carFuelConsumption = new CarFuelConsumption
                {
                    Brand = worksheet.Cells[i, 3].Value?.ToString(),
                    Model = worksheet.Cells[i, 4].Value?.ToString(),
                    CityConsumption = ConvertToLiterPer100Kilometer(worksheet.Cells[i, 10].Value),
                    HighwayConsumption = ConvertToLiterPer100Kilometer(worksheet.Cells[i, 11].Value),
                    FuelType = worksheet.Cells[i, 34].Value?.ToString()
                };

                CarFuelConsumptions.Add(carFuelConsumption);
            }
        }

        public static decimal ConvertToLiterPer100Kilometer(object cellValue)
            => 282.48m / Convert.ToDecimal(cellValue);
    }
    
}
#pragma warning restore CS8601 // Possible null reference assignment.
