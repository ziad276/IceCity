using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCity.Models
{
    public class GasHeater : Heater
    {
        public GasHeater(int power) : base(power)
        {
        }

        public override double CalculateEffectivePower()
        {
            return Power * 0.8;
        }
    }
}
