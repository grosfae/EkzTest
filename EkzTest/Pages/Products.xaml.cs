using EkzTest.Components.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EkzTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            LvProducts.ItemsSource = App.DB.Product.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductAddEditPage(new Product()));
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            NavigationService.Navigate(new ProductAddEditPage(selectedProduct));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            selectedProduct.IsDelete = true;
            App.DB.SaveChanges();
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = LvProducts.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            NavigationService.Navigate(new ProductBuyPage(selectedProduct));
            Refresh();
        }
        public void Refresh()
        {
            IEnumerable<Product> filterproducts = App.DB.Product;
            if (CbOrderB.SelectedIndex == 0)
            {
                filterproducts = filterproducts.ToList();
            }
            else if (CbOrderB.SelectedIndex == 1)
            {
                filterproducts = filterproducts.OrderBy(x => x.Name).ToList();
            }
            else if (CbOrderB.SelectedIndex == 2)
            {
                filterproducts = filterproducts.OrderByDescending(x => x.Name).ToList();
            }
            if (TbSearch.Text.Length > 0)
            {
                filterproducts = filterproducts.Where(x => x.Name.ToLower().StartsWith(TbSearch.Text.ToLower()));
            }
            LvProducts.ItemsSource = filterproducts.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void CbOrderB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
