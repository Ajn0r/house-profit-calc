using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using Newtonsoft.Json;

using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.IO;



namespace HouseProfitCalculator.Houses
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

        public void Serialize(int index, string fileName)
        {
            if (CheckIndex(index))
            {
                // serialize the house at the given index
                House house = houses[index];
                // serialize the house
                try
                {
                   string jsonString = JsonConvert.SerializeObject(house);
                   MessageBox.Show(jsonString);
                   File.WriteAllText(fileName, jsonString);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        private bool CheckIndex(int index)
        {
            if (index < 0 || index >= houses.Count)
            {
                return false;
            }
            return true;
        }

        public List<House> Deserialize()
        {
            throw new NotImplementedException();
        }

        public void LoadFrom()
        {
            throw new NotImplementedException();
        }

        public void AddHouse(House house)
        {
            if (house != null)
            {
                houses.Add(house);
                LatestHouse = house;
            }
        }
        public int GetIndexOfHouse(House house)
        {
            return houses.IndexOf(house);
        }
    }
}