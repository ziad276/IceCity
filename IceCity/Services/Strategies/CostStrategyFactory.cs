namespace IceCity.Services.Strategies
{
    public class CostStrategyFactory : ICostStrategyFactory
    {
        private readonly IEnumerable<ICostCalculationStrategy> _strategies;

        public CostStrategyFactory(IEnumerable<ICostCalculationStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ICostCalculationStrategy GetStrategy(string type)
        {
            var strategy = _strategies.FirstOrDefault(s =>
                s.StrategyType.Equals(type, StringComparison.OrdinalIgnoreCase));

            return strategy ?? throw new ArgumentException($"Unknown strategy type: {type}");
        }
    }
}