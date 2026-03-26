using IceCity.Models;


namespace IceCity.Services
{
    public class Report
    {
        private readonly ICostService _costService;

        public Report(ICostService costService)
        {
            _costService = costService;
        }

        public string GenerateMonthlyReport(House house)
        {
            if (house == null || house.DailyUsages.Count == 0)
            {
                return "No data available.";
            }

            int totalHours = _costService.CalculateTotalHours(house.DailyUsages);
            int median = _costService.CalculateMedian(house.DailyUsages);
            double cost = _costService.CalculateCost(totalHours, median, house.DailyUsages.Count);

            return $"Owner: {house.Owner.Name}\n" +
                   $"Total Working Hours: {totalHours} hours\n" +
                   $"Median Heater Value: {median} watts\n" +
                   $"Monthly Average Cost: ${cost:F2}";
        }
    }
}
