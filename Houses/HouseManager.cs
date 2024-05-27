using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using HouseProfitCalculator.Receipts;



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

        /// <summary>
        /// Method to serialize a house to a json file
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fileName"></param>
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

        /// <summary>
        /// Method to deserialize a house from a json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Deserialize(string fileName)
        {
            bool ok = false;
            if (File.Exists(fileName))
            {
                try
                {
                    string jsonString = File.ReadAllText(fileName);
                    House house = JsonConvert.DeserializeObject<House>(jsonString);
                    
                    // Adding the receipts to the ReceiptManager, at first the deserialize method did not add the receipts to the ReceiptManager so i created this soulution
                    // now it seemed to work with only the deserialize method, but i will keep this code here for now just in case
                    if (house != null)
                    {
                        if (house.Receipts != null)
                        {
                            // Incase the ReceiptManager is null, create a new instance
                            if (house.ReceiptManager == null)
                            {
                                house.ReceiptManager = new ReceiptManager();
                            }

                            // Using a temporary list to avoid modifying the list while iterating and getting the exception "Collection was modified; enumeration operation may not execute."
                            List<Receipt> receiptsToAdd = new List<Receipt>(house.Receipts);
                            // iterate through the receipts to add them to the ReceiptManager
                            foreach (Receipt receipt in receiptsToAdd)
                            {
                                // Check if the receipt is not already in the ReceiptManager and add it if it is not
                                if (!house.ReceiptManager.Receipts.Contains(receipt))
                                {
                                    house.ReceiptManager.AddReceipt(receipt);
                                }
                            }
                        }
                    }
                    // Adding the house to the houses list
                    houses.Add(house);
                    LatestHouse = house; // Setting the latest house to the house that was just added
                    ok = true; // return true if the deserialization was successful
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            // return if the deserialization was not successful
            return ok;
        }


        /// <summary>
        /// Method to check if the index is within the bounds of the houses list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool CheckIndex(int index)
        {
            if (index < 0 || index >= houses.Count)
            {
                return false;
            }
            return true;
        }

        public void AddHouse(House house)
        {
            if (house != null)
            {
                houses.Add(house);
                LatestHouse = house;
            }
        }

        /// <summary>
        /// Method to remove a house from the houses list, returns true if the house was removed
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        public bool RemoveHouse(House house)
        {
            // Check if the house is in the list
            if (houses.Contains(house))
            {
                houses.Remove(house);
                return true;
            }
            return false;
        }
        public int GetIndexOfHouse(House house)
        {
            return houses.IndexOf(house);
        }
    }
}