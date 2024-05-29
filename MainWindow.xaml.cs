using System.Text;
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

        // A Dictionary to keep track of the click count for each column header
        Dictionary<string, int> clickCountDict = new Dictionary<string, int>();


        public MainWindow()
        {
            InitializeComponent();
            houseManager = new HouseManager();
            //receiptManager = new ReceiptManager();
            categoryManager = new CategoryManager();
            // Set the data context of the window to the first house in the houseManager list and get the receiptManager from the house
            if (houseManager.Houses.Count > 0 && houseManager.Houses[0] != null)
            {
                House house = houseManager.Houses[0];
                this.DataContext = house;
                receiptManager = house.ReceiptManager;
            }
            SetButtonDisabled();
        }

        /// <summary>
        /// Method to set the buttons to disabled if there are no houses in the houseManager list
        /// </summary>
        private void SetButtonDisabled()
        {
            if (houseManager.Houses.Count == 0)
            {
                // Disable the buttons if there are no houses in the houseManager list
                editHouseBtn.IsEnabled = false;
                deleteHouseBtn.IsEnabled = false;
                newReceiptBtn.IsEnabled = false;
                editReceiptBtn.IsEnabled = false;
                deleteReceiptBtn.IsEnabled = false;
                exportBtn.IsEnabled = false;
            }
            else
            {
                // Enable the buttons if there are houses in the houseManager list
                editHouseBtn.IsEnabled = true;
                deleteHouseBtn.IsEnabled = true;
                newReceiptBtn.IsEnabled = true;
                editReceiptBtn.IsEnabled = true;
                deleteReceiptBtn.IsEnabled = true;
                exportBtn.IsEnabled = true;
            }
        }


        public void UpdateGUI(House selectedHouse)
        {
            SetHouseInComboBox(selectedHouse);
            receiptManager = selectedHouse.ReceiptManager;
            LoadReceipts();
            SetButtonDisabled();
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
            UpdateGUI(selectedHouse);
        }

        /// <summary>
        /// Method to handle when the EditHouseButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditHouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            // Get the selected house from the combo box and open a new EditHouseWindow with the selected house
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            EditHouseWindow editHouseWindow = new EditHouseWindow(houseManager, selectedHouse);
            // subscribe to the Closed event of the EditHouseWindow
            editHouseWindow.Closed += EditHouseWindow_Closed;
            editHouseWindow.Show();
        }

        /// <summary>
        /// Method to act when the EditHouseWindow is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EditHouseWindow_Closed(object sender, System.EventArgs e)
        {
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            this.DataContext = selectedHouse;
            UpdateGUI(selectedHouse);
        }

        /// <summary>
        /// Method to handle when the DeleteHouseButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteHouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Check that a house is selected in the combo box
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            // Get the house
            House selectedHouse = houseManager.Houses[cmbHouses.SelectedIndex];
            // Display a message box to confirm the deletion of the house so the user doesn't delete a house by mistake
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this house?", "Delete house", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            { // remove the house if the result is ues
                houseManager.RemoveHouse(selectedHouse);
            }
            // fill the house combo box and update the GUI
            FillHouseComboBox();
            if (houseManager.Houses.Count > 0)
            {
                // select the first house in the list as the selected house if the list is not empty and update the GUI
                House newSelectedHouse = houseManager.Houses[0];
                this.DataContext = newSelectedHouse;
                UpdateGUI(newSelectedHouse);
            }
        }

        /// <summary>
        /// Method to handle when the AddNewReceipt button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewReceipt_Clicked(object sender, RoutedEventArgs e)
        {
            NewReceiptWindow newReceiptWindow = new NewReceiptWindow(categoryManager, receiptManager);
            newReceiptWindow.Closed += NewReceiptWindow_Closed;
            newReceiptWindow.Show();
        }

        /// <summary>
        /// Method to act when the NewReceiptWindow is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewReceiptWindow_Closed(object? sender, EventArgs e)
        {
            LoadReceipts();
        }

        /// <summary>
        /// Method to load the receipts into the listbox
        /// </summary>
        private void LoadReceipts()
        {
            lstReceipts.Items.Clear();
            foreach (Receipt receipt in receiptManager.Receipts)
            {
                lstReceipts.Items.Add(receipt);
            }
        }

        /// <summary>
        /// Method to handle when the ExportButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportBtn_Clicked(object sender, RoutedEventArgs e)
        {
            // Get the selected house index from the combo box
            if (cmbHouses.SelectedIndex == -1)
            {
                return;
            }
            SaveFile();
        }

        private void SaveFile()
        {
            // Creates a new SaveFileDialog object and sets the filter to JSON files only
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            if (saveFileDialog.ShowDialog() == true)
            { // if the user selects a file, get the file path and serialize the selected house
                string filePath = saveFileDialog.FileName;
                int selectedHouseIndex = cmbHouses.SelectedIndex;
                houseManager.Serialize(selectedHouseIndex, filePath);
            }
        }

        /// <summary>
        /// Method to handle when the OpenFile button is clicked in the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Create a new OpenFileDialog object and set the filter to JSON files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            { // if the user selects a file, check the file path and deserialize the file
                string filePath = openFileDialog.FileName;
                if (houseManager.Deserialize(filePath))
                {
                    FillHouseComboBox();
                    UpdateGUI(houseManager.LatestHouse);
                }
                else
                {
                    MessageBox.Show("Could not open file, please try again with another file");
                }
            }
        }

        /// <summary>
        /// Method to handle when the EditReceiptButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditReceiptButton_Clicked(object sender, RoutedEventArgs e)
        {
            // check that a receipt is selected, if not show a message box
            if (lstReceipts.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a receipt to edit");
                return;
            }
            // Get the selected receipt and open a new EditReceiptWindow with the selected receipt
            Receipt selectedReceipt = receiptManager.Receipts[lstReceipts.SelectedIndex];
            EditReceiptWindow editReceiptWindow = new EditReceiptWindow(selectedReceipt, categoryManager, receiptManager);
            editReceiptWindow.Closed += EditReceiptWindow_Closed;
            editReceiptWindow.Show();
        }

        /// <summary>
        /// Method to handle when the EditReceiptWindow is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditReceiptWindow_Closed(object sender, System.EventArgs e)
        {
            LoadReceipts();
        }

        /// <summary>
        /// Method to handle when the DeleteReceiptButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            // check that a receipt is selected, if not show a message box
            if (lstReceipts.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a receipt to delete first");
                return;
            }
            // Get the selected receipt and display a message box to confirm the deletion
            Receipt selectedReceipt = receiptManager.Receipts[lstReceipts.SelectedIndex];
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this receipt?", "Delete receipt", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            { // remove the receipt if the result is yes
                if (receiptManager.RemoveReceipt(selectedReceipt))
                {
                    MessageBox.Show("Receipt deleted successfully");
                    LoadReceipts();
                } else
                {
                    MessageBox.Show("Could not delete receipt, please try again");
                }
            }
        }

        /// <summary>
        /// Method to handle when the SaveFile button is clicked in the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHouses.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select or create a house to save");
                return;
            }
            SaveFile();
        }

        /// <summary>
        /// Method to handle when the Exit button is clicked in the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Ask the user if they want to exit the application
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit the application?", "Exit application", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            { // if the result is yes, close the application
                Application.Current.Shutdown();
            }
        }

        private void SortByColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            // Get the coloumn header that was clicked
            GridViewColumnHeader column = (sender as GridViewColumnHeader); // sender is the column header that was clicked and cast it to a GridViewColumnHeader
            string sortBy = column.Content.ToString(); // Get the tag of the column header
            // Check if the click count dictionary contains the property name
            if (!clickCountDict.ContainsKey(sortBy))
                clickCountDict.Add(sortBy, 0); // If not, add the property name to the dictionary with a click count of 0
            int clickCount = clickCountDict[sortBy]; // Get the click count from the dictionary based on the property name and store it in a variable
            clickCount++; // Increment the click count
            clickCountDict[sortBy] = clickCount; // Set the click count in the dictionary to the new click count
            if (clickCount % 2 == 0) // If the click count is even, sort the list in descending order
                receiptManager.SortListDesc(sortBy); 
            else // If the click count is odd, sort the list in ascending order
                receiptManager.SortListAsc(sortBy);
             // Update the list view with the sorted list of receipts
             LoadReceipts();
        }
    }
}