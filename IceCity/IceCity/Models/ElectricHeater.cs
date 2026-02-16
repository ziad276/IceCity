using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCity.Models
{
    public class ElectricHeater : Heater
    {
        public ElectricHeater(int power) : base(power)
        {
        }

        public override double CalculateEffectivePower()
        {
            return Power;
        }
    }
}
