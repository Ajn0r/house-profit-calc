﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HouseProfitCalculator.Houses;
using HouseProfitCalculator.Receipts;
using Microsoft.Win32;

namespace HouseProfitCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HouseManager houseManager;

        private CategoryManager categoryManager;
        private ReceiptManager receiptManager;

        public MainWindow()
        {
            InitializeComponent();
            houseManager = new HouseManager();
            //receiptManager = new ReceiptManager();
            categoryManager = new CategoryManager();
            TestData();
            // Set the data context of the window to the first house in the houseManager list and get the receiptManager from the house
            if (houseManager.Houses.Count > 0 && houseManager.Houses[0] != null)
            {
                House house = houseManager.Houses[0];
                this.DataContext = house;
                receiptManager = house.ReceiptManager;
            }
        }

        private void TestData()
        {
            House house1 = new House();
            house1.Name = "Laxå";
            house1.Address = "Magnus Erikssons väg 4";
            house1.PurchasePrice = 475000;
            house1.AskingPrice = 950000;
            house1.ClosingCost = 60500;

            House house2 = new House();
            house2.Name = "Torved";
            house2.Address = "Torveds gård 4";
            house2.PurchasePrice = 830000;
            house2.AskingPrice = 2450000;
            house2.ClosingCost = 75000;

            House house3 = new House();
            house3.Name = "Hösbjör";
            house3.Address = "Hösbjörsvägen 213";
            house3.PurchasePrice = 1750000;
            house3.AskingPrice = 2730000;
            house3.ClosingCost = 56000;

            houseManager.AddHouse(house1);
            houseManager.AddHouse(house2);
            houseManager.AddHouse(house3);

            FillHouseComboBox();
            SetHouseInComboBox(house1);
        }

        public void UpdateGUI(House selectedHouse)
        {
            SetHouseInComboBox(selectedHouse);
            LoadReceipts();
        }

        /// <summary>
        /// Method that fills the combo box with the houses from the houseManager list
        /// </summary>
        private void FillHouseComboBox()
        {
            // Clear the combo box and add the houses to it from the houseManager list
            cmbHouses.Items.Clear();
            foreach (House house in houseManager.Houses)
            {
                cmbHouses.Items.Add(house.Name);
            }
        }

        /// <summary>
        /// Method to set the new house in the combo box, is used when a new house is added or the combo box selection is changed, the new house is passed as a parameter
        /// </summary>
        private void SetHouseInComboBox(House house)
        {
            // If the selected house is not null, set the selected item in the combo box to the name of the selected house
            if (house != null)
            {
                cmbHouses.SelectedItem = house.Name;
            }
        }

        /// <summary>
        /// Method to handle when the AddNewHouse button is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewHouse_Clicked(object sender, RoutedEventArgs e)
        {
            // open a new window to add a new house
            NewHouseWindow newHouseWindow = new NewHouseWindow(houseManager);
            // subscribe to the Closed event of the NewHouseWindow
            newHouseWindow.Closed += NewHouseWindow_Closed;
            newHouseWindow.Show();
        }

        /// <summary>
        /// Method to act when the NewHouseWindow is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewHouseWindow_Closed(object sender, System.EventArgs e)
        {
            // Cast the sender to a NewHouseWindow object to get the HouseAdded property 
            NewHouseWindow newHouseWindow = (NewHouseWindow)sender;
            // check if the house was added successfully
            if (!newHouseWindow.HouseAdded)
            {
                MessageBox.Show("House not added");
                return;
            } 

            // Display a message when the NewHouseWindow is closed
            MessageBox.Show("House added successfully");
            House newHouse = houseManager.LatestHouse;
            this.DataContext = newHouse;
            receiptManager = newHouse.ReceiptManager;
            FillHouseComboBox();
            UpdateGUI(newHouse);
        }

        /// <summary>
        /// Method to handle when the user selects a house from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HouseComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check the index
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            // Get the selected house from the combo box
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            // Set the datacontext of the window to the selected house and update the GUI with the selected house
            this.DataContext = selectedHouse;
            // Set the receipt manager of the selected house
            receiptManager = selectedHouse.ReceiptManager;
            UpdateGUI(selectedHouse);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            EditHouseWindow editHouseWindow = new EditHouseWindow(houseManager, selectedHouse);
            editHouseWindow.Closed += EditHouseWindow_Closed;
            editHouseWindow.Show();
        }
        public void EditHouseWindow_Closed(object sender, System.EventArgs e)
        {
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            this.DataContext = selectedHouse;
            UpdateGUI(selectedHouse);
        }

        private void AddNewReceipt_Clicked(object sender, RoutedEventArgs e)
        {
            NewReceiptWindow newReceiptWindow = new NewReceiptWindow(categoryManager, receiptManager);
            newReceiptWindow.Closed += NewReceiptWindow_Closed;
            newReceiptWindow.Show();
        }

        private void NewReceiptWindow_Closed(object? sender, EventArgs e)
        {
            LoadReceipts();
        }

        private void LoadReceipts()
        {
            lstReceipts.Items.Clear();
            foreach (Receipt receipt in receiptManager.Receipts)
            {
                lstReceipts.Items.Add(receipt);
            }
        }

        private void ExportBtn_Clicked(object sender, RoutedEventArgs e)
        {
            // Get the selected house index from the combo box
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                int selectedHouseIndex = cmbHouses.SelectedIndex;
                //House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
                houseManager.Serialize(selectedHouseIndex, filePath);
            }
            
            // Serialize the houseManager object to a JSON file
            //houseManager.Serialize(selectedHouseIndex);

        }
    }
}