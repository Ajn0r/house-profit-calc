using HouseProfitCalculator.Helper_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HouseProfitCalculator.Receipts
{
    public class ReceiptManager
    {
        private List<Receipt> receipts;

        public List<Receipt> Receipts
        {
            get { return receipts; }
        }
        public ReceiptManager()
        {
            receipts = new List<Receipt>();
        }

        public event EventHandler ReceiptAdded;
        public event EventHandler ReceiptRemoved;

        /// <summary>
        /// Method to add a receipt to the list of receipts
        /// </summary>
        /// <param name="receipt"></param>
        public void AddReceipt(Receipt receipt)
        {
            if (receipt != null)
            {
                receipts.Add(receipt);
                ReceiptAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Method to remove a receipt from the list of receipts
        /// </summary>
        /// <param name="selectedReceipt"></param>
        public bool RemoveReceipt(Receipt selectedReceipt)
        {
            if (receipts.Contains(selectedReceipt))
            {
                receipts.Remove(selectedReceipt);
                ReceiptRemoved?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to update a receipt in the list of receipts
        /// </summary>
        /// <param name="oldReceipt"></param>
        /// <param name="newReceipt"></param>
        public void UpdateReceipt(Receipt oldReceipt, Receipt newReceipt)
        {
            if (receipts.Contains(oldReceipt))
            {
                int index = receipts.IndexOf(oldReceipt);
                receipts[index] = newReceipt;
                ReceiptAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Method to sort the list of receipts in descending order
        /// </summary>
        /// <param name="sortBy"></param>
        internal void SortListDesc(string sortBy)
        {
            receipts.Sort(new ReceiptComparer(sortBy, descending: true));
        }

        /// <summary>
        /// Method to sort the list of receipts in ascending order
        /// </summary>
        /// <param name="sortBy"></param>
        internal void SortListAsc(string sortBy)
        {
            receipts.Sort(new ReceiptComparer(sortBy, descending: false));
        }
    }
}