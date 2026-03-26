namespace IceCity.Models
{
    public class SolarHeater : Heater
    {
        public SolarHeater(int power) : base(power)
        {
        }   

        public override double CalculateEffectivePower()
        {
            return Power;
        }
    }
}
