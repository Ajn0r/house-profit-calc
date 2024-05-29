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
        private Calculator calculator;

        public event PropertyChangedEventHandler PropertyChanged;

        public House()
        {
            receiptManager = new ReceiptManager();
            calculator = new Calculator();
            receiptManager.ReceiptAdded += OnReceiptChange;
            receiptManager.ReceiptRemoved += OnReceiptChange;
        }

        public House(string name, string address, double purchasePrice, double askingPrice, double closingCost)
        {
            Name = name;
            Address = address;
            PurchasePrice = purchasePrice;
            AskingPrice = askingPrice;
            ClosingCost = closingCost;
            receiptManager = new ReceiptManager();
            calculator = new Calculator();
            receiptManager.ReceiptAdded += OnReceiptChange;
            receiptManager.ReceiptRemoved += OnReceiptChange;
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

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Event handler for when a receipt is added, changed or removed from the receiptManager list, updates the properties for the house object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnReceiptChange(object sender, EventArgs args)
        {
            OnPropertyChanged(nameof(Spendings));
            OnPropertyChanged(nameof(Profit));
            OnPropertyChanged(nameof(Tax));
            OnPropertyChanged(nameof(NetProfit));
        }

        public double Profit
        {
            get
            {
                CalculateValues();
                return calculator.CalculateProfit();
            }
        }

        public double Tax
        {
            get
            {
                CalculateValues();
                return calculator.CalculateTax();
            }
        }

        public double Spendings
        {
            get
            {
                CalculateValues();
                return calculator.CalculateSpendings();
            }
        }

        public double NetProfit
        {
            get
            {
                CalculateValues();
                return calculator.CalculateNetProfit();
            }
        }


        /// <summary>
        /// Method to calculate the values for the house object, sends the values to the calculator object to calculate the profit, tax, spendings and net profit
        /// </summary>
        private void CalculateValues()
        {
            calculator.CalculateValues(PurchasePrice, AskingPrice, ClosingCost, Receipts);
        }

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
            set
            {
                // Unsubscribe from the old receiptManager events in case it is not null before setting the new value
                if (receiptManager != null)
                {
                    receiptManager.ReceiptAdded -= OnReceiptChange;
                }
                receiptManager = value; // set the new receiptManager value
                // Subscribe to the new receiptManager events
                if (receiptManager != null)
                {
                    receiptManager.ReceiptAdded += OnReceiptChange;
                }
                OnPropertyChanged();
            }
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