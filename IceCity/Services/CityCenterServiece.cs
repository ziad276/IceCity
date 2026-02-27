using IceCity.Models;

namespace IceCity.Services
{
    public class CityCenterService
    {
        public static async Task<Heater?> RequestReplacementAsync(int houseId, int heaterId)
        {
            Console.WriteLine("City Center Was Contacted");
            await Task.Delay(1000);
            Console.WriteLine("City Center Replaced Heater");
            Random random = new Random();
            if (random.Next(0, 2) == 0) 
            {
                Console.WriteLine("Replacement available!");
                return new ElectricHeater(5000);
            }
            else
            {
                Console.WriteLine("No replacement available.");
                return null;
            }
        }
    }
}
