using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using Sales.WPFApp.Models;

namespace Sales.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SaleWindow : Window
    {
        private int id;

        public SaleWindow()
        {
            InitializeComponent();
            InitComboBox();
            InitDataGrid();
        }

        public SaleWindow(int id)
        {
            InitializeComponent();
            this.id = id;
            InitDetails();
            InitComboBox();
            InitDataGrid();
        }

        async void InitDetails()
        {
            Sale sale = await Sale.Find(this.id);
            if (sale != null)
            {
                comboBoxClient.SelectedValue = sale.ClientId;
                datePickerSale.SelectedDate = sale.DateSale;
            }
        }

        async void InitComboBox()
        {
            comboBoxClient.ItemsSource = await Client.ToList();
            comboBoxProduct.ItemsSource = await Product.ToList();
        }

        async void InitDataGrid()
        {
            if (this.id <= 0)
            {
                dataGrid.ItemsSource = new List<SaleProduct>();
                dataGrid.SelectionMode = DataGridSelectionMode.Single;
                dataGrid.IsReadOnly = true;
            }
            else
            {
                dataGrid.ItemsSource = (await Sale.Find(this.id)).SaleProducts;
                dataGrid.SelectionMode = DataGridSelectionMode.Single;
                dataGrid.IsReadOnly = true;
            }
        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)comboBoxClient.SelectedItem;
            DateTime? date = datePickerSale.SelectedDate;
            List<SaleProduct> saleProducts = dataGrid.Items.OfType<SaleProduct>().ToList();
            if (client != null && date.HasValue && saleProducts.Count > 0)
            {
                Sale sale = this.id > 0 ? await Sale.Find(id) : new Sale();
                sale.ClientId = client.Id;
                sale.DateSale = date.Value;
                sale.SaleProducts = saleProducts;
                HttpResponseMessage response = this.id > 0 ? await Sale.Edit(sale) : await Sale.Add(sale);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Sale of {client.Name} was recorded");
                }
                else
                {
                    MessageBox.Show($"Sale of {client.Name} wasn't recorded");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a client, enter a date and add some products before...");
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)comboBoxProduct.SelectedItem;
            int quantity = int.Parse(textBoxQuantity.Text.Trim());
            if (product != null && quantity > 0)
            {
                if (product.Stock >= quantity)
                {
                    SaleProduct saleProduct = new SaleProduct();
                    saleProduct.Product = product;
                    saleProduct.ProductId = product.Id;
                    saleProduct.Quantity = quantity;
                    saleProduct.Price = quantity * product.Price;
                    List<SaleProduct> saleProducts = dataGrid.Items.OfType<SaleProduct>().ToList();
                    saleProducts.Add(saleProduct);
                    dataGrid.ItemsSource = saleProducts;
                }
                else
                {
                    MessageBox.Show("Stock insufficient...");
                }

            }
            else
            {
                MessageBox.Show("Select a product and enter a quantity before...");
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            SaleProduct saleProduct = (SaleProduct)dataGrid.SelectedItem;
            if (saleProduct != null)
            {
                saleProduct.ProductId = (int)comboBoxProduct.SelectedValue;
                saleProduct.Product = (Product)comboBoxProduct.SelectedItem;
                saleProduct.Quantity = int.Parse(textBoxQuantity.Text.Trim());
                saleProduct.Price = saleProduct.Quantity * saleProduct.Product.Price;

                List<SaleProduct> saleProducts = dataGrid.Items.OfType<SaleProduct>().ToList();
                saleProducts.RemoveAt(dataGrid.SelectedIndex);
                saleProducts.Add(saleProduct);
                dataGrid.ItemsSource = saleProducts;
            }
            else
            {
                MessageBox.Show("Select a item...");
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            SaleProduct saleProduct = (SaleProduct)dataGrid.SelectedItem;
            if (saleProduct != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Item", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<SaleProduct> saleProducts = dataGrid.Items.OfType<SaleProduct>().ToList();
                    saleProducts.RemoveAt(dataGrid.SelectedIndex);
                    dataGrid.ItemsSource = saleProducts;
                }
            }
            else
            {
                MessageBox.Show("Select a product...");
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaleProduct saleProduct = (SaleProduct)dataGrid.SelectedItem;
            if (saleProduct != null)
            {
                comboBoxProduct.SelectedValue = saleProduct.ProductId;
                textBoxQuantity.Text = saleProduct.Quantity.ToString();
            }
        }
    }
}
