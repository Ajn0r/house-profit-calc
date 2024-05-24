using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HouseProfitCalculator
{
    public class Receipt : INotifyPropertyChanged
    {
        private DateOnly purchaseDate;
        private double amount;
        private string store;
        private string imgPath;
        private string category;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateOnly PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; OnPropertyChanged(); }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }
        public string Store
        {
            get { return store; }
            set { store = value; OnPropertyChanged(); }
        }
        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; OnPropertyChanged(); }
        }
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}