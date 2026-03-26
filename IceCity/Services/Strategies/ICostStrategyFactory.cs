namespace IceCity.Services.Strategies
{
    public interface ICostStrategyFactory
    {
        ICostCalculationStrategy GetStrategy(string type);
    }
}
