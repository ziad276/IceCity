using IceCity.Models;
using IceCity.Services.Strategies;

namespace IceCity.Services
{
    public class CostService : ICostService
    {
        private readonly ICostStrategyFactory _strategyFactory;

        public CostService(ICostStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public int CalculateTotalHours(List<DailyUsage> dailyUsages)
        {
            int total = 0;
            foreach (var usage in dailyUsages)
            {
                total += usage.Hours;
            }

            return total;
        }

        public int CalculateMedian(List<DailyUsage> dailyUsages)
        {
            var heaterValues = dailyUsages.Select(u => u.HeaterValue).OrderBy(v => v).ToList();
            return heaterValues[heaterValues.Count / 2];
        }

        public double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays)
        {
            var strategy = _strategyFactory.GetStrategy("standard");
            return strategy.CalculateCost(totalWorkingTime, medianHeaterValue, numberOfDays);
        }

        public double CalculateMonthlyCost(List<DailyUsage> dailyUsages, string strategyType)
        {
            if (dailyUsages == null || dailyUsages.Count == 0)
            {
                throw new InvalidOperationException("No daily usage data available");
            }

            int totalHours = CalculateTotalHours(dailyUsages);
            int medianHeaterValue = CalculateMedian(dailyUsages);
            int numberOfDays = dailyUsages.Count;

            var strategy = _strategyFactory.GetStrategy(strategyType);
            return strategy.CalculateCost(totalHours, medianHeaterValue, numberOfDays);
        }
    }
}
