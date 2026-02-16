using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCity.Models
{
    public abstract class Heater
    {
        private int _power;

        public Heater(int power)
        {
            Power = power;
        }

        public int Power
        {
            get { return _power; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Power must be non-negative");
                }
                _power = value;
            }
        }
        public abstract double CalculateEffectivePower();

    }
}
