using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseProfitCalculator
{
    public class CategoryManager
    {
        // A list to store all the categories with a private set to prevent modification except from within the class
        public List<string> Categories { get; private set; }

        // Constructor to initialize the list with all the categories from the Type enum
        public CategoryManager()
        {
            Categories = Enum.GetNames(typeof(Type)).ToList();
        }

        // Method to add a new category to the list
        public void AddCategory(string category)
        {
            // Check if the category is not already in the list and if it is not an empty string
            if (!Categories.Contains(category) && !string.IsNullOrEmpty(category))
            {
                Categories.Add(category);
            } 
        }
    }
}
