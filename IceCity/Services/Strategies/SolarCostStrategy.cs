namespace IceCity.Services.Strategies
{
    public class SolarCostStrategy : BaseCostStrategy
    {
        public override string StrategyType => "solar";

        public override double CalculateCost(int totalWorkingTime, int medianHeaterValue, int numberOfDays)
        {
            double cost = medianHeaterValue * ((double)totalWorkingTime / (24 * numberOfDays));
            return cost * 0.7;
        }
    }
}
