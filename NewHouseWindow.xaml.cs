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

namespace HouseProfitCalculator
{
    /// <summary>
    /// Interaction logic for NewHouseWindow.xaml
    /// </summary>
    public partial class NewHouseWindow : Window
    {
        HouseManager houseManager;
        public NewHouseWindow(HouseManager houseManager)
        {
            InitializeComponent();
            this.houseManager = houseManager;
        }

        private void AddHouseBtn_Click(object sender, RoutedEventArgs e)
        {
            House newHouse = new House();
            newHouse.Name = txtName.Text;
            newHouse.Adress = txtAddress.Text;
            newHouse.PurchasePrice = double.Parse(txtPurchasePrice.Text);
            newHouse.AskingPrice = double.Parse(txtAskingPrice.Text);
            newHouse.ClosingCost = double.Parse(txtClosingCost.Text);
            houseManager.AddHouse(newHouse);
            this.Close();
        }
    }
}
