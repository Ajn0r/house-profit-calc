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
            // Display a message when the NewHouseWindow is closed
            MessageBox.Show("House added successfully");
            this.DataContext = houseManager.LatestHouse;
        }

    }
}