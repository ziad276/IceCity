using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IceCity.Models;

namespace IceCity.Services
{
    public class WeatherService
    {
        public static async Task<List<DailyUsage>> FetchLastMonthWeatherAsync()
        {
            List<DailyUsage> usages = new List<DailyUsage>();

            DateTime now = DateTime.UtcNow;
            DateTime start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime end = new DateTime(now.Year, now.Month, 1).AddDays(-1);

            string url = $"https://archive-api.open-meteo.com/v1/archive?" +
                         $"latitude=31.0409&longitude=31.3785" +
                         $"&start_date={start:yyyy-MM-dd}" +
                         $"&end_date={end:yyyy-MM-dd}" +
                         $"&daily=temperature_2m_max,temperature_2m_min,precipitation_sum";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);

            using var json = JsonDocument.Parse(response);
            var daily = json.RootElement.GetProperty("daily");

            var dates = daily.GetProperty("time").EnumerateArray();
            var maxTemps = daily.GetProperty("temperature_2m_max").EnumerateArray();
            var minTemps = daily.GetProperty("temperature_2m_min").EnumerateArray();
            var rain = daily.GetProperty("precipitation_sum").EnumerateArray();

            while (dates.MoveNext() && maxTemps.MoveNext() && minTemps.MoveNext() && rain.MoveNext())
            {
                DateTime date = DateTime.Parse(dates.Current.GetString());

                int hours = 12;
                int heaterValue = 4000;

                var usage = new DailyUsage(date, hours, heaterValue);
                usages.Add(usage);
            }

            return usages;
        }
    }
}