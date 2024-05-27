using HouseProfitCalculator.Helper_classes;
using HouseProfitCalculator.Receipts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HouseProfitCalculator
{
    /// <summary>
    /// Interaction logic for NewReceiptWindow.xaml
    /// </summary>
    public partial class NewReceiptWindow : Window
    {
        private CategoryManager categoryManager;
        private ReceiptManager receiptManager;
        private string imagePath;
        public NewReceiptWindow(CategoryManager categoryManager, ReceiptManager receiptManager)
        {
            InitializeComponent();
            this.categoryManager = categoryManager;
            this.receiptManager = receiptManager;
            FillCategoryComboBox();
        }

        private void FillCategoryComboBox()
        {
            cmbCategory.Items.Clear();
            foreach (string category in categoryManager.Categories)
            {
                cmbCategory.Items.Add(category);
            }
        }

        private void AddReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();
            Receipt receipt = new Receipt();

            // Validate all fields
            if (!InputValidator.ValidateReceiptInput(txtAmount.Text, txtStore.Text, cmbCategory.SelectedItem.ToString(), ref errors))
            {
                if (dpDate.SelectedDate == null)
                    errors.Add("You need to select a date");
                InputValidator.DisplayErrorMessage(ref errors, "receipt");
                return;
            } 
            if (string.IsNullOrEmpty(imagePath)) // if the image path is empty
            {
                // Ask the user if they are sure they want to add a receipt without an image
                MessageBoxResult result = MessageBox.Show("Are you sure you want to add a receipt without an image?", "No image added", MessageBoxButton.YesNo);
                // If the user clicks no, return from the method
                if (result == MessageBoxResult.No)
                    return;
            }
            receipt.Amount = double.Parse(txtAmount.Text);
            receipt.Store = txtStore.Text;
            receipt.Category = cmbCategory.SelectedItem.ToString();
            receipt.PurchaseDate = DateOnly.FromDateTime(dpDate.SelectedDate.Value); // Convert the selected date to a DateOnly object and save it to the receipt
            receipt.ImgPath = imagePath;

            receiptManager.AddReceipt(receipt);
            this.Close();
        }

        /// <summary>
        /// Method to handle when the user wants to add an image to the receipt. Same solution as for WildlifeTracker project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addImage_Clicked(object sender, RoutedEventArgs e)
        {
            // Create a new OpenFileDialog to let the user select an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Uri fileUri = null; // Create a new Uri to hold the image path to convert to a bitmap image
            openFileDialog.Filter = "(*.bmp;*.jpg;*.jpeg;*.png;*.gif;)|*.bmp;*.jpg;*.jpeg;*.png;*.gif;"; // Set the filter to only show image files
            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                fileUri = new Uri(imagePath); // Create a new Uri with the image path
            } 
            if (!string.IsNullOrEmpty(imagePath)) // Check that the image path is not empty
            {
                imgReceipt.Source = new BitmapImage(fileUri); // Create a new bitmap image with the Uri
            }
        }

        /// <summary>
        /// Method to handle when the user wants to add a new category to the list of categories
        /// Opens a messagebox to let the user input the new category name instead of creating a new window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            // Using a messagebox to let the user input the new category name 
            string newCategory = Microsoft.VisualBasic.Interaction.InputBox("Enter the new category", "New Category", "");
            // Check that the user has entered a new category
            if (!string.IsNullOrEmpty(newCategory.Trim()))
            {
                // add the new category to the list of categories
                categoryManager.AddCategory(newCategory);
                FillCategoryComboBox(); // fill the combobox with the new category
                cmbCategory.SelectedItem = newCategory; // set the new category as the selected item
            } else // show a messagebox if the user has not entered a new category
            {
                MessageBox.Show("You need to enter a category name");
            }
        }
    }
}
