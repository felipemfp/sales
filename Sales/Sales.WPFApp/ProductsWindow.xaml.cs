using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();
            InitComboBox();
            InitDataGrid();
        }

        async void InitComboBox()
        {
            comboBoxManufacturer.ItemsSource = await Manufacturer.ToList();
        }

        async void InitDataGrid()
        {
            dataGrid.ItemsSource = await Product.ToList();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            int manufacturerId = ((Manufacturer)comboBoxManufacturer.SelectedItem).Id;
            string description = textBoxDescription.Text.Trim();
            decimal price = decimal.Parse(textBoxPrice.Text.Trim(), new CultureInfo("pt-BR"));
            int stock = int.Parse(textBoxStock.Text.Trim());
            if (manufacturerId > 0 && !String.IsNullOrEmpty(description))
            {
                Product product = new Product()
                {
                    ManufacturerId = manufacturerId,
                    Description = description,
                    Price = price,
                    Stock = stock
                };
                HttpResponseMessage responde = await Product.Add(product);
                if (responde.IsSuccessStatusCode)
                {
                    InitDataGrid();
                    MessageBox.Show($"Product {product.Description} was added");
                }
                else
                {
                    MessageBox.Show($"Product {product.Description} wasn't added");
                }
            }
            else
            {
                MessageBox.Show("All fields are required...");
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)dataGrid.SelectedItem;

            int newManufacturerId = ((Manufacturer)comboBoxManufacturer.SelectedItem).Id;
            string newDescription = textBoxDescription.Text.Trim();
            decimal newPrice = decimal.Parse(textBoxPrice.Text.Trim(), new CultureInfo("pt-BR"));
            int newStock = int.Parse(textBoxStock.Text.Trim());

            if (newManufacturerId > 0 && !String.IsNullOrEmpty(newDescription))
            {
                product.ManufacturerId = newManufacturerId;
                product.Description = newDescription;
                product.Price = newPrice;
                product.Stock = newStock;
                HttpResponseMessage responde = await Product.Edit(product);
                if (responde.IsSuccessStatusCode)
                {
                    InitDataGrid();
                    MessageBox.Show($"Product #{product.Id} was edited");
                }
                else
                {
                    MessageBox.Show($"Product #{product.Id} wasn't edited");
                }
            }
            else
            {
                MessageBox.Show("All fields are required...");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)dataGrid.SelectedItem;
            if (product != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Product", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HttpResponseMessage responde = await Product.Delete(product);
                    if (responde.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        MessageBox.Show($"Product #{product.Id} was deleted");
                    }
                    else
                    {
                        MessageBox.Show($"Product #{product.Id} wasn't deleted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a product...");
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)dataGrid.SelectedItem;
            if (product != null)
            {
                textBoxDescription.Text = product.Description;
                textBoxPrice.Text = product.Price.ToString("0,##", new CultureInfo("pt-BR"));
                textBoxStock.Text = product.Stock.ToString();
                comboBoxManufacturer.SelectedValue = product.ManufacturerId;
            }
        }
    }
}
