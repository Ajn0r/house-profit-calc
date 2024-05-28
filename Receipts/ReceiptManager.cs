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

        public void UpdateReceipt(Receipt oldReceipt, Receipt newReceipt)
        {
            if (receipts.Contains(oldReceipt))
            {
                int index = receipts.IndexOf(oldReceipt);
                receipts[index] = newReceipt;
                ReceiptAdded?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}