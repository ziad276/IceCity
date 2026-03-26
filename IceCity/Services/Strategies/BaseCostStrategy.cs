using System;
using System.Collections.Generic;
using IceCity.Models;

namespace IceCity.Services.Strategies
{
    public abstract class BaseCostStrategy : ICostCalculationStrategy 
    {
        public abstract string StrategyType { get; }

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
            List<int> heaterValues = new List<int>();  
            foreach (var usage in dailyUsages)
            {
                heaterValues.Add(usage.HeaterValue);
            }

            int n = heaterValues.Count;  

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (heaterValues[i] > heaterValues[j])
                    {
                        int temp = heaterValues[i];
                        heaterValues[i] = heaterValues[j];
                        heaterValues[j] = temp;
                    }
                }
            }

            int median = heaterValues[n / 2];


            return median;
        }

        public abstract double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays);
    }
}