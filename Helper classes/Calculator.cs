using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseProfitCalculator.Receipts;

namespace HouseProfitCalculator
{
    public class Calculator
    {
        private double profit;
        private double spendings;

        public void CalculateValues(double purchasePrice, double askingPrice, double closingCost, List<Receipt> receipts)
        {
            profit = askingPrice - (purchasePrice + closingCost);
            spendings = SumAllReceipts(receipts);
        }

        public double CalculateTax()
        {
            double netProfit = profit - spendings;
            if (netProfit * 0.22 < 0)
                return 0;
            return netProfit * 0.22;
        }

        public double CalculateProfit()
        {
            return profit - spendings;
        }

        public double CalculateSpendings()
        {
            return spendings;
        }

        public double CalculateNetProfit()
        {
            double tax = CalculateTax();
            return profit - spendings - tax;
        }

        private double SumAllReceipts(List<Receipt> receipts)
        {
            double totalSpendings = 0;
            foreach (var receipt in receipts)
            {
                totalSpendings += receipt.Amount;
            }
            return totalSpendings;
        }
    }
}