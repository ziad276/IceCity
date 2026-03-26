namespace IceCity.Services.Strategies
{
    public class StandardCostStrategy : BaseCostStrategy
    {
        public override string StrategyType => "standard";

        public override double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays)
        {
            double averageCost = medianHeaterValue * ((double)totalWorkingTime / (24 * numberOfDays));
            return averageCost;
        }
    }
}
