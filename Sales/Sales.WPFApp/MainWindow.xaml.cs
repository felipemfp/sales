using System.Windows;
using System.Windows.Controls;
using Sales.WPFApp.Models;

namespace Sales.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        async void InitDataGrid()
        {
            dataGridVips.ItemsSource = await Client.Vips();
            dataGridVips.SelectionMode = DataGridSelectionMode.Single;
            dataGridVips.IsReadOnly = true;
            dataGridProducts.ItemsSource = await Product.TopSelling();
            dataGridProducts.SelectionMode = DataGridSelectionMode.Single;
            dataGridProducts.IsReadOnly = true;
        }

        private void buttonSales_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow salesWindow = new SalesWindow();
            salesWindow.ShowDialog();
        }

        private void buttonClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void buttonProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow();
            productsWindow.ShowDialog();
        }

        private void buttonManufacturers_Click(object sender, RoutedEventArgs e)
        {
            ManufacturersWindow manufacturersWindow = new ManufacturersWindow();
            manufacturersWindow.ShowDialog();
        }

        private async void textBoxLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            int length = 0;
            int.TryParse(textBoxLength.Text, out length);
            if (length > 0 && dataGridProducts != null)
            {
                dataGridProducts.ItemsSource = await Product.TopSelling(length);
            }
        }
    }
}
