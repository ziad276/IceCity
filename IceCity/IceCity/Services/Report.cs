using IceCity.Models;


namespace IceCity.Services
{
    public class Report
    {
        public string GenerateMonthlyReport(House house)
        {
            if (house == null || house.DailyUsages.Count == 0)
            {
                return "No data available.";
            }

            CalculationService service = new CalculationService();

            int totalHours = service.CalculateWorkingTime(house.DailyUsages);
            int median = service.CalculateMedianValue(house.DailyUsages);
            double cost = service.CalculateAverageCost(totalHours, median, house.DailyUsages.Count);

            return $"Owner: {house.Owner.Name}\n" +
                   $"Total Working Hours: {totalHours} hours\n" +
                   $"Median Heater Value: {median} watts\n" +
                   $"Monthly Average Cost: ${cost:F2}";
        }
    }
}
