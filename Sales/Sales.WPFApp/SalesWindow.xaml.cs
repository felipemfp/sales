using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Sales.WPFApp.Models;

namespace Sales.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        public SalesWindow()
        {
            InitializeComponent();
            InitDataGrid();
            InitComboBox();
        }

        private void ClearFilterPerDate()
        {
            datePickerStart.SelectedDate = null;
            datePickerEnd.SelectedDate = null;
        }

        private void ClearFilterPerClient()
        {
            comboBoxClient.SelectedIndex = -1;
        }

        private async void InitComboBox()
        {
            comboBoxClient.ItemsSource = await Client.ToList();
        }

        private async void InitDataGrid()
        {
            dataGrid.ItemsSource = await Sale.ToList();
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }

        private void buttonNew_Click(object sender, RoutedEventArgs e)
        {
            SaleWindow saleWindow = new SaleWindow();
            saleWindow.ShowDialog();
            InitDataGrid();
            ClearFilterPerDate();
            ClearFilterPerClient();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Sale sale = (Sale)dataGrid.SelectedItem;
            if (sale != null)
            {
                SaleWindow saleWindow = new SaleWindow(sale.Id);
                saleWindow.ShowDialog();
                InitDataGrid();
                ClearFilterPerDate();
                ClearFilterPerClient();
            }
            else
            {
                MessageBox.Show("Select a sale...");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Sale sale = (Sale)dataGrid.SelectedItem;
            if (sale != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Sale", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HttpResponseMessage responde = await Sale.Delete(sale);
                    if (responde.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFilterPerDate();
                        ClearFilterPerClient();
                        MessageBox.Show($"Sale #{sale.Id} of {sale.Client.Name} was deleted");
                    }
                    else
                    {
                        MessageBox.Show($"Sale #{sale.Id} of {sale.Client.Name} wasn't deleted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a sale...");
            }
        }

        private async void comboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = (Client)comboBoxClient.SelectedItem;
            if (client != null)
            {
                dataGrid.ItemsSource = await Sale.ToList(client);
                ClearFilterPerDate();
            }
        }

        private async void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? startDate = datePickerStart.SelectedDate;
            DateTime? endDate = datePickerEnd.SelectedDate;
            if (startDate.HasValue && endDate.HasValue)
            {
                dataGrid.ItemsSource = await Sale.ToList(startDate.Value, endDate.Value);
                ClearFilterPerClient();
            }
        }
    }
}
