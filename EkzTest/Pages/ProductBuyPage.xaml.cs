using EkzTest.Components.Model;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ProductBuyPage.xaml
    /// </summary>
    public partial class ProductBuyPage : Page
    {
        Product contextProduct;
        public ProductBuyPage(Product product)
        {
            InitializeComponent();
            contextProduct = product;
            DataContext = contextProduct;
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            int cou = int.Parse(TbCount.Text);
            string errorMessage = "";
            if (cou <= 0 || cou > 10000)
            {
                errorMessage += "Введите корректное количество\n";
            }
            if (string.IsNullOrEmpty(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }
            contextProduct.Count = contextProduct.Count - cou;
            App.DB.SaveChanges();
            NavigationService.GoBack();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
