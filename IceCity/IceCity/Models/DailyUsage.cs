using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IceCity.Models
{
    public class DailyUsage
    {
        private int _hours;
        private int _heaterValue;
        public DailyUsage(DateTime date, int hours, int heaterValue)
        {
            Date = date; 
            Hours = hours;
            HeaterValue = heaterValue;
        }
        private DateTime Date { get; set; }
       

        public int Hours
        {
            get { return _hours; }
            set
            {
                if (value < 0 || value > 24)
                {
                    throw new ArgumentException("Hours must be between 0 and 24");
                }
                _hours = value;
            }
        }

        public int HeaterValue
        {
            get { return _heaterValue; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Heater value must be non-negative");
                }
                _heaterValue = value;
            }
        }

    }
}

