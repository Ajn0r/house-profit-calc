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

namespace HouseProfitCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HouseManager houseManager;
        public MainWindow()
        {
            InitializeComponent();
            houseManager = new HouseManager();
        }

        public void UpdateGUI(House selectedHouse)
        {
            FillHouseComboBox();
            SetHouseInComboBox(selectedHouse);
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
            // check if the house was added successfully
            if (houseManager.LatestHouse == null)
            {
                return;
            }
            // Display a message when the NewHouseWindow is closed
            MessageBox.Show("House added successfully");
            House newHouse = houseManager.LatestHouse;
            this.DataContext = newHouse;
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
            SetHouseInComboBox(selectedHouse);
        }
    }
}