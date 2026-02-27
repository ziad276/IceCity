namespace IceCity.Models
{
    public abstract class Heater
    {
        private int _power;
        private DateTime? _lastOpenTime;
        private int _nextHeaterId = 1;

        public event HeaterOpenDelegate OnHeaterOpen;
        public event CloseHeaterDelegate OnHeaterClose;
        public int HeaterId { get; private set; }  

        public Heater(int power)
        {
            HeaterId = _nextHeaterId++;
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

        public void Open()
        {
            _lastOpenTime = DateTime.UtcNow;
            OnHeaterOpen?.Invoke(this, _lastOpenTime.Value);
        }

        
        public void Close()
        {
            if (_lastOpenTime == null)
            {
                throw new InvalidOperationException("Heater must be opened before it can be closed.");
            }
            DateTime closeTime = DateTime.Now;
            TimeSpan duration = closeTime - _lastOpenTime.Value;
            double hoursWorked = duration.TotalHours;

            OnHeaterClose?.Invoke(this, hoursWorked, Power);

            _lastOpenTime = null;
        }

    }

    public delegate void HeaterOpenDelegate(Heater heater, DateTime open);
    public delegate void CloseHeaterDelegate(Heater heater, double hoursWorked, int heaterValue);
}
