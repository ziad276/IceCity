

namespace IceCity.Models
{
    public class Owner
    {
        private string _name;
        private List<House> _houses;
        public int Id { get; set; }
        private int _age;

        public Owner(string name)
        {
            Name = name;
            _houses = new List<House>();

        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                _name = value;
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age cannot be negative");
                }
                _age = value;
            }
        }

        public List<House> Houses
        {
            get { return _houses; }
        }

        public void AddHouse(House house)
        {
            if (house == null)
            {
                throw new ArgumentNullException(nameof(house));
            }
            _houses.Add(house);
        }
    }
}
       
    

    

