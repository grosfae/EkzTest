using EkzTest.Components.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EkzTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductAddEditPage.xaml
    /// </summary>
    public partial class ProductAddEditPage : Page
    {
        Product contextProduct;
        public ProductAddEditPage(Product product)
        {
            InitializeComponent();
            contextProduct = product;
            DataContext = contextProduct;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextProduct.Name))
            {
                errorMessage += "Введите ммя\n";
            }
            if (contextProduct.Count <= 0 || contextProduct.Count > 10000)
            {
                errorMessage += "Введите корректное количество\n";
            }
            if (contextProduct.Price < 0 || contextProduct.Price == null || contextProduct.Price > 15000)
            {
                errorMessage += "Введите корректную цену\n";
            }
            if (string.IsNullOrEmpty(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }
            if (contextProduct.Id == 0)
            {
                App.DB.Product.Add(contextProduct);
            }
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if(dialog.ShowDialog().GetValueOrDefault())
            {
                contextProduct.Image = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextProduct;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9.]") == false)
            {
                e.Handled = true;
            }
        }
    }
}
