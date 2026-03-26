using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IceCity.Models;
using IceCity.Services;
using IceCity.Services.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace IceCity
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<ICostCalculationStrategy, StandardCostStrategy>();
            services.AddTransient<ICostCalculationStrategy, EcoCostStrategy>();
            services.AddTransient<ICostCalculationStrategy, SolarCostStrategy>();
            services.AddTransient<ICostStrategyFactory, CostStrategyFactory>();
            services.AddTransient<ICostService, CostService>();
            services.AddTransient<Report>();

            var serviceProvider = services.BuildServiceProvider();
            var costService = serviceProvider.GetRequiredService<ICostService>();

            Console.WriteLine("========================================");
            Console.WriteLine("   WELCOME TO ICECITY HEATING SYSTEM   ");
            Console.WriteLine("========================================\n");

            // --- Owner and House ---
            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine();
            Owner owner = new Owner(ownerName);
            House house = new House(owner);
            Console.WriteLine($"\nHouse created for {owner.Name} (House ID: {house.HouseId})");

            // --- Add Heaters ---
            Console.Write("\nHow many heaters does the house have? ");
            int heaterCount = int.Parse(Console.ReadLine());

            for (int i = 1; i <= heaterCount; i++)
            {
                Console.WriteLine($"\n--- Heater {i} ---");
                Console.Write("Type (1=Electric, 2=Gas, 3=Solar): ");
                int type = int.Parse(Console.ReadLine());
                Console.Write("Power (watts): ");
                int power = int.Parse(Console.ReadLine());

                if (type == 1)
                    house.AddHeater(new ElectricHeater(power));
                else if (type == 2)
                    house.AddHeater(new GasHeater(power));
                else if (type == 3)
                    house.AddHeater(new SolarHeater(power));
                else
                    house.AddHeater(new ElectricHeater(power));

                Console.WriteLine($"✓ Heater added (ID: {house.Heaters[house.Heaters.Count - 1].HeaterId})");
            }

            // --- Save DailyUsage from heater events ---
            house.SaveUsageAction = (usage) => house.DailyUsages.Add(usage);

            // --- Simulate 5 heater operations ---
            Console.WriteLine("\n--- Simulating 5 Heater Operations ---");
            for (int i = 0; i < 5; i++)
            {
                foreach (var heater in house.Heaters)
                {
                    heater?.Open();
                    Thread.Sleep(50);  // Simulate heating
                    heater?.Close();   // Creates DailyUsage
                }
            }
            Console.WriteLine($"✓ Created {house.DailyUsages.Count} usage records from heater events");

            // --- Generate 30 days of random data ---
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

            // --- Choose Cost Strategy ---
            Console.WriteLine("\nChoose cost strategy: 1=Standard, 2=Eco, 3=Solar");
            int choice = int.Parse(Console.ReadLine());
            string strategyType = choice switch
            {
                2 => "eco",
                3 => "solar",
                _ => "standard"
            };

            // --- Calculate total hours, median, and cost ---
            int totalHours = costService.CalculateTotalHours(house.DailyUsages);
            int medianHeaterValue = costService.CalculateMedian(house.DailyUsages);
            int numberOfDays = house.DailyUsages.Count;
            double totalCost = costService.CalculateMonthlyCost(house.DailyUsages, strategyType);

            Console.WriteLine("\n--- Monthly Heating Cost ---");
            Console.WriteLine($"Total Hours: {totalHours}");
            Console.WriteLine($"Median Heater Value: {medianHeaterValue}");
            Console.WriteLine($"Number of Days: {numberOfDays}");
            Console.WriteLine($"Total Cost ({strategyType}): {totalCost:C}");

            Console.WriteLine("\n--- Extension Method Demo ---");
            int testNumber = 42;
           // Console.WriteLine($"Is {testNumber} even? {testNumber.IsEven()}");
            testNumber = 17;
            //Console.WriteLine($"Is {testNumber} even? {testNumber.IsEven()}");

            Console.WriteLine("\n========================================");
            Console.WriteLine("   ALL FEATURES DEMONSTRATED!          ");
            Console.WriteLine("========================================");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}