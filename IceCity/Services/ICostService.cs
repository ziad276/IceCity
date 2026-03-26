using IceCity.Models;

namespace IceCity.Services
{
    public interface ICostService
    {
        int CalculateTotalHours(List<DailyUsage> dailyUsages);
        int CalculateMedian(List<DailyUsage> dailyUsages);
        double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays);
        double CalculateMonthlyCost(List<DailyUsage> dailyUsages, string strategyType);
    }
}
