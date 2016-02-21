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
using System.Windows.Shapes;

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
    }
}
