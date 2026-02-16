using System;
using IceCity.Models;
using IceCity.Services;
using IceCity.Models;


namespace IceCity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   WELCOME TO ICECITY HEATING SYSTEM   ");
            Console.WriteLine("========================================\n");

            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine();
            Owner owner = new Owner(ownerName);

            House house = new House(owner);
            Console.WriteLine($"\nHouse created for {owner.Name}");

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
                    Console.WriteLine($"Electric heater ({power}W) added.");
                }
                else if (type == 2)
                {
                    house.AddHeater(new GasHeater(power));
                    Console.WriteLine($"Gas heater ({power}W) added.");
                }
                else
                {
                    Console.WriteLine("Invalid type. Defaulting to Electric.");
                    house.AddHeater(new ElectricHeater(power));
                }
            }

            Console.WriteLine("\n--- Generating 30 Days of Usage Data ---");
            
            
                Random random = new Random();
                for (int day = 1; day <= 30; day++)
                {
                    int hours = random.Next(8, 24);  
                    int heaterValue = random.Next(3000, 6000); 
                    DateTime date = DateTime.Now.AddDays(day - 30);

                    house.AddDailyUsage(new DailyUsage(date, hours, heaterValue));
                }
                Console.WriteLine("✓ 30 days of sample data generated automatically.");
            

            Console.WriteLine("\n--- Generating Report ---\n");
            Report report = new Report();
            string result = report.GenerateMonthlyReport(house);

            Console.WriteLine(result);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}