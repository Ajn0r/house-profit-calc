using HouseProfitCalculator.Helper_classes;
using HouseProfitCalculator.Houses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HouseProfitCalculator
{
    /// <summary>
    /// Interaction logic for NewHouseWindow.xaml
    /// </summary>
    public partial class EditHouseWindow : Window
    {
        private HouseManager houseManager;
        private House houseToEdit;
  
        public EditHouseWindow(HouseManager houseManager, House house)
        {
            InitializeComponent();
            this.houseManager = houseManager;
            houseToEdit = house;
            // Set the input fields to the values of the house object
            this.DataContext = houseToEdit;
        }

        /// <summary>
        /// Method to handle when the AddHouse button is clicked, validates the input fields and creates a new house object if the input is valid
        /// Uses the InputValidator class to validate the input fields and display an error message if the input is invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateHouseBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errorList = new List<string>();

            // Validate the input fields
            if (!InputValidator.ValidateHouseInput(txtName.Text, txtAddress.Text, txtPurchasePrice.Text, txtAskingPrice.Text, txtClosingCost.Text, ref errorList))
            {
                // Display the error message
                InputValidator.DisplayErrorMessage(ref errorList, "house");
                // Clear the error list
                errorList.Clear();
                return;
            }
            // else if the input is valid, update the house object
            houseToEdit.Name = txtName.Text;
            houseToEdit.Address = txtAddress.Text;
            houseToEdit.PurchasePrice = double.Parse(txtPurchasePrice.Text);
            houseToEdit.AskingPrice = double.Parse(txtAskingPrice.Text);
            houseToEdit.ClosingCost = double.Parse(txtClosingCost.Text);
            
            // Close the window after the house is added
            this.Close();
        }
    }
}
