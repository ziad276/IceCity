using IceCity.Models;

namespace IceCity.Services.Strategies
{
    public interface ICostCalculationStrategy
    {
        string StrategyType { get; }
        double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays);
        int CalculateMedian(List<DailyUsage> dailyUsages);
        int CalculateTotalHours(List<DailyUsage> dailyUsages);
    }
}
