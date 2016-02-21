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
    public partial class ManufacturersWindow : Window
    {
        public ManufacturersWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void ClearFields()
        {
            textBoxDescription.Text = string.Empty;
        }

        private async void InitDataGrid()
        {
            dataGrid.ItemsSource = await Manufacturer.ToList();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string description = textBoxDescription.Text.Trim();
            if (!String.IsNullOrEmpty(description))
            {
                Manufacturer manufacturer = new Manufacturer()
                {
                    Description = description
                };
                HttpResponseMessage response = await Manufacturer.Add(manufacturer);
                if (response.IsSuccessStatusCode)
                {
                    InitDataGrid();
                    ClearFields();
                    MessageBox.Show($"Manufacturer {manufacturer.Description} was added");
                }
                else
                {
                    MessageBox.Show($"Manufacturer {manufacturer.Description} wasn't added");
                }
            }
            else
            {
                MessageBox.Show("Enter a description...");
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = (Manufacturer)dataGrid.SelectedItem;
            if (manufacturer != null)
            {
                string newDescription = textBoxDescription.Text.Trim();
                if (!String.IsNullOrEmpty(newDescription))
                {
                    manufacturer.Description = newDescription;
                    HttpResponseMessage response = await Manufacturer.Edit(manufacturer);
                    if (response.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFields();
                        MessageBox.Show($"Manufacturer {manufacturer.Description} was edited");
                    }
                    else
                    {
                        MessageBox.Show($"Manufacturer {manufacturer.Description} wasn't edited");
                    }
                }
                else
                {
                    MessageBox.Show("Description is required...");
                }
            }
            else
            {
                MessageBox.Show("Select a manufacturer...");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = (Manufacturer)dataGrid.SelectedItem;
            if (manufacturer != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Manufacturer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HttpResponseMessage response = await Manufacturer.Delete(manufacturer);
                    if (response.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFields();
                        MessageBox.Show($"Manufacturer {manufacturer.Description} was deleted");
                    }
                    else
                    {
                        MessageBox.Show($"Manufacturer {manufacturer.Description} wasn't deleted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a manufacturer...");
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Manufacturer manufacturer = (Manufacturer)dataGrid.SelectedItem;
            if (manufacturer != null)
            {
                textBoxDescription.Text = manufacturer.Description;
            }
        }
    }
}
