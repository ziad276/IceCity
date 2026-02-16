using IceCity.Services;

namespace IceCity.Models
{
    public class House
    {
        private Owner _owner;
        private List<Heater> _heaters;
        private List<DailyUsage> _dailyUsages;
        public House(Owner owner)
        {
            _owner = owner;
            _heaters = new List<Heater>();       
            _dailyUsages = new List<DailyUsage>(); 
        }
        public Owner Owner { get { return _owner; } }
        public List<Heater> Heaters { get { return _heaters; } }
        public List<DailyUsage> DailyUsages { get { return _dailyUsages; } }

        public void AddHeater(Heater heater)
        {
            if (heater == null)
            {
                throw new ArgumentNullException(nameof(heater));
            }
            Heaters.Add(heater);
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
    }
}