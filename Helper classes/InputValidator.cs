using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseProfitCalculator.Helper_classes
{
    public class InputValidator
    {
        /// <summary>
        /// Method that checks if the input is a valid string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsStringValid(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Method that checks if the input is a valid int number, and if it is greater than or equal to 0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberValid(string input)
        {
            bool okInt = int.TryParse(input, out int result);
            if (result >= 0) // check that the number is greater than, or equal to 0
                return okInt; // return the result of the parse
            else // if the number is less than 0, return false no matter the result of the parse
                return false;
        }

        /// <summary>
        /// Method that checks if the input is a valid double, and if it is greater than or equal to 0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDoubleValid(string input)
        {
            bool okDouble = double.TryParse(input, out double result);
            if (result >= 0) // check that the number is greater than, or equal to 0
                return okDouble; // return the result of the parse
            else // if the number is less than 0, return false no matter the result of the parse
                return false;
        }

        /// <summary>
        /// Method to display a error message if the object could not be created, takes a list of errors and the type of object that could not be created as parameters
        /// </summary>
        /// <param name="errorList"></param>
        /// <param name="type"></param>
        public static void DisplayErrorMessage(ref List<string> errorList, string type)
        {
            string errors = $"The {type} could not be created. \n";
            // Set the error message info based on the number of errors
            if (errorList.Count > 1)
                errors += "The following errors occured: \n";
            else
                errors += "The following error occured: \n";
            // Then loop through the error list and add the errors to the error message
            foreach (string error in errorList)
            {
                errors += "- " + error + "\n";
            }
            MessageBox.Show(errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to validate the input fields for the house in one single method, and add the errors to the error list
        /// Also adds a helpful message to the user if they are unsure of the values they should enter in the purchase price, asking price and closing cost fields
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="purchasePrice"></param>
        /// <param name="askingPirce"></param>
        /// <param name="closingCost"></param>
        /// <param name="errorList"></param>
        /// <returns></returns>
        public static bool ValidateHouseInput(string name, string address, string purchasePrice, string askingPirce, string closingCost,ref List<string> errorList)
        {
            List<String> msg = new List<string>();

            // check if the input fields are valid and add the errors to the error list if they are not
            if (!IsStringValid(name))
                errorList.Add("The name field cannot be empty");
            if (!IsStringValid(address))
                errorList.Add("The address field cannot be empty");
            if (!IsDoubleValid(purchasePrice))
            {
                errorList.Add("The purchase price must be a number greater than or equal to 0");
                msg.Add("the purchase price");
            }
            if (!IsDoubleValid(askingPirce))
            {
                errorList.Add("The asking price must be a number greater than or equal to 0");
                msg.Add("the asking price");
            }
            if (!IsDoubleValid(closingCost))
            {
                errorList.Add("The closing cost must be a number greater than or equal to 0");
                msg.Add("the closing cost");
            }
            // Custom message for the user if they are unsure of the values they should enter to help them
            if (msg.Count == 2)
                errorList.Add("If your are unsure of " + string.Join(" or ", msg) + " just enter 0");
            else if (msg.Count == 1)
                errorList.Add("If you are unsure of " + msg + " just enter 0");
            else if (msg.Count == 3)
                errorList.Add("If you are unsure of " + msg[0] + ", " + msg[1] + " or " + msg[2] + " just enter 0");
            
            // return true if the error list is empty, and false if it is not
            return errorList.Count == 0;
        }
    }
}
