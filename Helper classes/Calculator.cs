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

        /// <summary>
        /// Method that takes all the values needed to calculate the profit, spendings, tax and net profit of the house object
        /// calculates the profit and spendings and stores them in the class variables
        /// </summary>
        /// <param name="purchasePrice"></param>
        /// <param name="askingPrice"></param>
        /// <param name="closingCost"></param>
        /// <param name="receipts"></param>
        public void CalculateValues(double purchasePrice, double askingPrice, double closingCost, List<Receipt> receipts)
        {
            profit = askingPrice - (purchasePrice + closingCost);
            spendings = SumAllReceipts(receipts);
        }

        /// <summary>
        /// Method that returns the tax of the house object
        /// </summary>
        /// <returns></returns>
        public double CalculateTax()
        {
            double netProfit = profit - spendings;
            if (netProfit * 0.22 < 0)
                return 0;
            return netProfit * 0.22;
        }

        /// <summary>
        /// Method that returns the profit of the house object
        /// </summary>
        /// <returns></returns>
        public double CalculateProfit()
        {
            return profit - spendings;
        }

        /// <summary>
        /// Method that returns the spendings of the house object
        /// </summary>
        /// <returns></returns>
        public double CalculateSpendings()
        {
            return spendings;
        }

        /// <summary>
        /// Method to calculate the net profit of the house object by subtracting the tax from the profit and spendings
        /// </summary>
        /// <returns></returns>
        public double CalculateNetProfit()
        {
            double tax = CalculateTax();
            return profit - spendings - tax;
        }

        /// <summary>
        /// Method to sum all the receipts in the list of receipts
        /// </summary>
        /// <param name="receipts"></param>
        /// <returns></returns>
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