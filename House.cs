using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HouseProfitCalculator
{
    public class House
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public double PurchasePrice { get; set; }
        public double AskingPrice { get; set; }
        public double ClosingCost { get; set; }
        public double Profit { get; set; }
        public double FinalTax { get; set; }
        public List<Receipt> Receipts { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}, Address: {Adress}, Purchase price: {PurchasePrice}";
        }
    }
}