using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using HouseProfitCalculator.Receipts;
using Newtonsoft.Json;

namespace HouseProfitCalculator.Houses
{
    /// <summary>
    /// House class that holds all information about a house object. implements INotifyPropertyChanged to notify the UI when a property is changed, fount
    /// the inspiration for that solution here: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/how-to-implement-property-change-notification?view=netframeworkdesktop-4.8
    /// </summary>
    [Serializable]
    public class House : INotifyPropertyChanged
    {
        private string name;
        private string address;
        private double purchasePrice;
        private double askingPrice;
        private double closingCost;
        private ReceiptManager receiptManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public House()
        {
            receiptManager = new ReceiptManager();
        }

        public House(string name, string address, double purchasePrice, double askingPrice, double closingCost)
        {
            Name = name;
            Address = address;
            PurchasePrice = purchasePrice;
            AskingPrice = askingPrice;
            ClosingCost = closingCost;
            receiptManager = new ReceiptManager();
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        public double PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; OnPropertyChanged(); }
        }

        public double AskingPrice
        {
            get { return askingPrice; }
            set { askingPrice = value; OnPropertyChanged(); }
        }

        public double ClosingCost
        {
            get { return closingCost; }
            set { closingCost = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public double Profit { get; set; }
        public double FinalTax { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Address: {Address}, Purchase price: {PurchasePrice}";
        }

        /// <summary>
        /// Method to get the receiptmanager for each house object to be able to add receipts to the receiptmanager list connected to the house object
        /// Is ignored in the serialization process, uses the Receipts property below instead
        /// </summary>
        [JsonIgnore]
        public ReceiptManager ReceiptManager
        {
            get { return receiptManager; }
            private set { receiptManager = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Method to get the receipts from the receiptManager list for serialization
        /// </summary>
        public List<Receipt> Receipts
        {
            get { return receiptManager?.Receipts; }
        }

    }
}