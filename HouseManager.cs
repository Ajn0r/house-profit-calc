using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseProfitCalculator
{
    public class HouseManager
    {
        private List<House> houses;

        public HouseManager()
        {
            houses = new List<House>();
        }

        public Calculator Calculator
        {
            get => default;
            set
            {
            }
        }

        public List<House> Houses
        {
            get => houses;
            set
            {
            }
        }

        public House LatestHouse { get; private set; }

        public void Serialize()
        {
            throw new System.NotImplementedException();
        }

        public List<House> Deserialize()
        {
            throw new System.NotImplementedException();
        }

        public void LoadFrom()
        {
            throw new System.NotImplementedException();
        }

        public void AddHouse(House house)
        {
            if (house != null)
            {
                houses.Add(house);
                LatestHouse = house;
            }
        }
    }
}