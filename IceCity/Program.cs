using System;
using System.Threading;
using System.Threading.Tasks;
using IceCity.Models;
using IceCity.Services;

namespace IceCity
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   WELCOME TO ICECITY HEATING SYSTEM   ");
            Console.WriteLine("========================================\n");



            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine();
            Owner owner = new Owner(ownerName);
            House house = new House(owner);
            Console.WriteLine($"\nHouse created for {owner.Name} (House ID: {house.HouseId})");

           
            Console.Write("\nHow many heaters does the house have? ");
            int heaterCount = int.Parse(Console.ReadLine());

            for (int i = 1; i <= heaterCount; i++)
            {
                Console.WriteLine($"\n--- Heater {i} ---");
                Console.Write("Type (1=Electric, 2=Gas): ");
                int type = int.Parse(Console.ReadLine());
                Console.Write("Power (watts): ");
                int power = int.Parse(Console.ReadLine());

                if (type == 1)
                {
                    house.AddHeater(new ElectricHeater(power));
                    Console.WriteLine($"✓ Electric heater ({power}W) added (ID: {house.Heaters[house.Heaters.Count - 1].HeaterId})");
                }
                else if (type == 2)
                {
                    house.AddHeater(new GasHeater(power));
                    Console.WriteLine($"✓ Gas heater ({power}W) added (ID: {house.Heaters[house.Heaters.Count - 1].HeaterId})");
                }
                else
                {
                    Console.WriteLine("Invalid type. Defaulting to Electric.");
                    house.AddHeater(new ElectricHeater(power));
                }
            }

            
            house.SaveUsageAction = (usage) =>
            {
                house.DailyUsages.Add(usage);
            };

           
            Console.WriteLine("\n--- Simulating 5 Heater Operations ---");
            for (int i = 0; i < 5; i++)
            {
                foreach (var heater in house.Heaters)
                {
                    if (heater != null)
                    {
                        heater.Open();
                        Thread.Sleep(50);  // Simulate heating
                        heater.Close();    // This fires event → creates DailyUsage
                    }
                }
            }
            Console.WriteLine($"✓ Created {house.DailyUsages.Count} usage records from heater events");


            Console.WriteLine("\n--- Generating Additional 30 Days of Data ---");
            Random random = new Random();
            for (int day = 1; day <= 30; day++)
            {
                int hours = random.Next(8, 24);
                int heaterValue = random.Next(3000, 6000);
                DateTime date = DateTime.Now.AddDays(day - 30);
                house.AddDailyUsage(new DailyUsage(date, hours, heaterValue));
            }
            Console.WriteLine($"✓ Generated 30 days of random data");
            Console.WriteLine($"✓ Total usage records: {house.DailyUsages.Count}");

          
            Console.WriteLine("\n--- Generating Monthly Report ---\n");
            Report report = new Report();
            string result = report.GenerateMonthlyReport(house);
            Console.WriteLine(result);

           
            Console.WriteLine("\n--- Fetching Real Weather Data from API ---");
            Console.WriteLine("Please wait, calling weather API...\n");

            try
            {
                var weatherData = await WeatherService.FetchLastMonthWeatherAsync();
                Console.WriteLine($"✅ SUCCESS! Fetched {weatherData.Count} days of real weather data!");

                Console.WriteLine("\nFirst 5 days of weather data:");
                for (int i = 0; i < Math.Min(5, weatherData.Count); i++)
                {
                    var w = weatherData[i];
                    Console.WriteLine($"  {w.Date:yyyy-MM-dd} | Hours={w.Hours} | HeaterValue={w.HeaterValue}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to fetch weather data: {ex.Message}");
            }

            
            Console.WriteLine("\n--- Testing Thread Printing ---");
            PrintService printService = new PrintService();
            printService.PrintLastMonthDailyUsageWithThreads(house);

            
            Console.WriteLine("\n--- Testing Task Printing ---");
            await printService.PrintLastMonthDailyUsageWithTasks(house);

            
            Console.WriteLine("\n--- Testing Exception Handling ---");
            Console.WriteLine("Attempting to open first heater (may fail)...\n");

            if (house.Heaters.Count > 0)
            {
                try
                {
                    await house.TryOpenHeaterAsync(0);
                    Console.WriteLine("✓ Heater opened successfully (or replaced if failed)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }

            Console.WriteLine("\n--- Extension Method Demo ---");
            int testNumber = 42;
            Console.WriteLine($"Is {testNumber} even? {testNumber.IsEven()}");

            testNumber = 17;
            Console.WriteLine($"Is {testNumber} even? {testNumber.IsEven()}");

         
            Console.WriteLine("\n========================================");
            Console.WriteLine("   ALL FEATURES DEMONSTRATED!          ");
            Console.WriteLine("========================================");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}