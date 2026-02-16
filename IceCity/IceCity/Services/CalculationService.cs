using IceCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCity.Services
{


    public class CalculationService 
    {
        
        public int CalculateWorkingTime(List<DailyUsage> dailyUsages)
        {
            int n = dailyUsages.Count();
            int total = 0;
            foreach (var usage in dailyUsages)
            {
                total += usage.Hours;
            }
            return total;
        }


        public int CalculateMedianValue(List<DailyUsage> dailyUsages)
        {

            List<int> HeaterValues = new List<int>();
            foreach (var usage in dailyUsages)
            {
                HeaterValues.Add(usage.HeaterValue);
            }
            int n = HeaterValues.Count();
            for(int i = 0; i < n; i++)
            {
                for(int j = i +1 ; j < n; j++)
                {
                    if (HeaterValues[i] > HeaterValues[j])
                    {
                        int temp = HeaterValues[i];
                        HeaterValues[i] = HeaterValues[j];
                        HeaterValues[j] = temp;
                    }

                }
            }
            int median = HeaterValues[n / 2];
            return median;
        }

        public double CalculateAverageCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays)
        {
            double averageCost = medianHeaterValue * ((double)totalWorkingTime / (24 * numberOfDays));
            return averageCost;
        }
    }

   
}

