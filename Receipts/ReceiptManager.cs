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
        public void AddReceipt(Receipt receipt)
        {
            if (receipt != null)
            {
                receipts.Add(receipt);
            }
        }
    }
}