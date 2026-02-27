using IceCity.Services;

namespace IceCity.Models
{
    public class House
    {
        private Owner _owner;
        private List<Heater?> _heaters;
        private List<DailyUsage> _dailyUsages;
        private static int _nextHouseId = 1;

        public SaveDailyUsageDelegate SaveUsageAction { get; set; }

        public House(Owner owner)
        {
            _owner = owner;
            _heaters = new List<Heater?>();       
            _dailyUsages = new List<DailyUsage>();
            HouseId = _nextHouseId++;
        }
        public Owner Owner { get { return _owner; } }
        public List<Heater?> Heaters { get { return _heaters; } }
        public List<DailyUsage> DailyUsages { get { return _dailyUsages; } }
        public int HouseId { get; private set; }  


        public void AddHeater(Heater heater)
        {
            if (heater == null)
            {
                throw new ArgumentNullException(nameof(heater));
            }
            Heaters.Add(heater);
            heater.OnHeaterClose += HandleHeaterClose;
        }

        private void HandleHeaterClose(Heater heater, double hoursWorked, int heaterValue)
        {
            var usage = new DailyUsage(
               DateTime.UtcNow,           
               (int)hoursWorked,          
               heaterValue                
                    );

            SaveUsageAction?.Invoke(usage);     
        }

        public void AddDailyUsage(DailyUsage usage)
        {
            if (usage == null)
            {
                throw new ArgumentNullException(nameof(usage));
            }
            DailyUsages.Add(usage);
        }


        public double CalculateMonthlyCost()
        {
            if (DailyUsages.Count == 0)
            {
                throw new InvalidOperationException("No daily usage data available");
            }

            CalculationService service = new CalculationService();

            int totalHours = service.CalculateWorkingTime(DailyUsages);
            int medianValue = service.CalculateMedianValue(DailyUsages);
            int numberOfDays = DailyUsages.Count;

            double cost = service.CalculateAverageCost(totalHours, medianValue, numberOfDays);

            return cost;
        }
        public async Task TryOpenHeaterAsync(int heaterIndex)
        {
            if (heaterIndex < 0 || heaterIndex >= _heaters.Count)
            {
                Console.WriteLine("Invalid heater index.");
                return;
            }

            Heater? heater = _heaters[heaterIndex];

            if (heater == null)
            {
                Console.WriteLine($"Heater at position {heaterIndex} is null. Skipping.");
                return;
            }

            try
            {
                heater.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Heater {heater.HeaterId} failed! Requesting replacement...");

                Heater? replacement = await CityCenterService.RequestReplacementAsync(HouseId, heater.HeaterId);

                _heaters[heaterIndex] = replacement;

                if (replacement != null)
                {
                    replacement.OnHeaterClose += HandleHeaterClose;
                    Console.WriteLine($"Replacement heater installed.");
                }
            }
        }
    }
    public delegate void SaveDailyUsageDelegate(DailyUsage usage);

}