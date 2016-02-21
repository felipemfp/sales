using System;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
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

        private void ClearFields()
        {
            comboBoxManufacturer.SelectedIndex = -1;
            textBoxDescription.Text = string.Empty;
            textBoxStock.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
        }

        private async void InitComboBox()
        {
            comboBoxManufacturer.ItemsSource = await Manufacturer.ToList();
        }

        private async void InitDataGrid()
        {
            dataGrid.ItemsSource = await Product.ToList();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = (Manufacturer)comboBoxManufacturer.SelectedItem;
            int manufacturerId = manufacturer.Id;
            string description = textBoxDescription.Text.Trim();
            decimal price = -1;
            decimal.TryParse(textBoxPrice.Text.Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out price);
            int stock = -1;
            int.TryParse(textBoxStock.Text.Trim(), out stock);
            if (manufacturerId > 0 && !String.IsNullOrEmpty(description) && stock >= 0 && price >= 0)
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
                    ClearFields();
                    MessageBox.Show($"Product {product.Description} of {manufacturer.Description} was added");
                }
                else
                {
                    MessageBox.Show($"Product {product.Description} of {manufacturer.Description} wasn't added");
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
            if (product != null)
            {
                int newManufacturerId = ((Manufacturer)comboBoxManufacturer.SelectedItem).Id;
                string newDescription = textBoxDescription.Text.Trim();
                decimal newPrice = -1;
                decimal.TryParse(textBoxPrice.Text.Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out newPrice);
                int newStock = -1;
                int.TryParse(textBoxStock.Text.Trim(), out newStock);

                if (newManufacturerId > 0 && !String.IsNullOrEmpty(newDescription) && newStock >= 0 && newPrice >= 0)
                {
                    product.ManufacturerId = newManufacturerId;
                    product.Description = newDescription;
                    product.Price = newPrice;
                    product.Stock = newStock;
                    HttpResponseMessage responde = await Product.Edit(product);
                    if (responde.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFields();
                        MessageBox.Show($"Product {product.Description} of {product.Manufacturer.Description} was edited");
                    }
                    else
                    {
                        MessageBox.Show($"Product {product.Description} of {product.Manufacturer.Description} wasn't edited");
                    }
                }
                else
                {
                    MessageBox.Show("All fields are required...");
                }
            }
            else
            {
                MessageBox.Show("Select a product...");
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
                        ClearFields();
                        MessageBox.Show($"Product {product.Description} of {product.Manufacturer.Description} was deleted");
                    }
                    else
                    {
                        MessageBox.Show($"Product {product.Description} of {product.Manufacturer.Description} wasn't deleted");
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
                textBoxPrice.Text = product.Price.ToString("0.00", new CultureInfo("pt-BR"));
                textBoxStock.Text = product.Stock.ToString();
                comboBoxManufacturer.SelectedValue = product.ManufacturerId;
            }
        }
    }
}
