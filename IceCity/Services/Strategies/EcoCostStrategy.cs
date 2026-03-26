namespace IceCity.Services.Strategies
{
    public class EcoCostStrategy : BaseCostStrategy
    {
        public override string StrategyType => "eco";

        public override double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays)
        {
            double averageCost = medianHeaterValue * ((double)totalWorkingTime / (24 * numberOfDays));
            if (totalWorkingTime < 120)
            {
                return averageCost * 0.9;
            }
            return averageCost;
        }
    }
}
