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

        async void InitDataGrid()
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
                await Manufacturer.Add(manufacturer);
                InitDataGrid();
            }
            else
            {
                MessageBox.Show("Enter a description...");
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = dataGrid.SelectedItem as Manufacturer;
            string newDescription = textBoxDescription.Text.Trim();
            if (manufacturer != null && !String.IsNullOrEmpty(newDescription))
            {
                manufacturer.Description = newDescription;
                await Manufacturer.Edit(manufacturer);
                InitDataGrid();
            }
            else
            {
                MessageBox.Show("Description is required...");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = dataGrid.SelectedItem as Manufacturer;
            if (manufacturer != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Manufacturer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await Manufacturer.Delete(manufacturer);
                    InitDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Select a manufacturer...");
            }
        }
    }
}
