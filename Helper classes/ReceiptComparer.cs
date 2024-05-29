using HouseProfitCalculator.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseProfitCalculator.Helper_classes
{
    public class ReceiptComparer : IComparer<Receipt>
    {
        private string sortBy;
        private bool descending;

        public ReceiptComparer(string sortBy, bool descending)
        {
            this.sortBy = sortBy;
            this.descending = descending;
        }

        /// <summary>
        /// Method to compare two receipts based on the sortBy value and the descending value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Receipt? x, Receipt? y)
        {
            int comparison = 0;

            // check if it should be descending or ascending and compare the values accordingly
            if (descending)
            {
                switch (sortBy.ToLower())
                {
                    case "amount":
                        comparison = y.Amount.CompareTo(x.Amount);
                        break;
                    case "store":
                        comparison = string.Compare(y.Store, x.Store);
                        break;
                    case "date":
                        comparison = y.PurchaseDate.CompareTo(x.PurchaseDate);
                        break;
                    case "category":
                        comparison = string.Compare(y.Category, x.Category);
                        break;
                    default:
                        MessageBox.Show("Invalid sort by value");
                        break;
                }

            }
            else { 
                switch (sortBy.ToLower())
                {
                    case "amount":
                        comparison = x.Amount.CompareTo(y.Amount);
                        break;
                    case "store":
                        comparison = string.Compare(x.Store, y.Store);
                        break;
                    case "date":
                        comparison = x.PurchaseDate.CompareTo(y.PurchaseDate);
                        break;
                    case "category":
                        comparison = string.Compare(x.Category, y.Category);
                        break;
                    default:
                        MessageBox.Show("Invalid sort by value");
                        break;
                } 
            }

            return comparison;
        }
    }

}
