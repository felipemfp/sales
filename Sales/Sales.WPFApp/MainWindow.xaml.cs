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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        async void InitDataGrid()
        {
            HttpResponseMessage response = await APIService.GetClient().GetAsync("clients");
            string json = response.Content.ReadAsStringAsync().Result;
            dataGrid.ItemsSource = JsonConvert.DeserializeObject<List<Client>>(json);
            dataGrid.IsReadOnly = true;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            if (!String.IsNullOrEmpty(name))
            {
                Client client = new Client()
                {
                    Name = name,
                    VIP = false
                };
                string json = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await APIService.GetClient().PostAsync("clients", content);
                InitDataGrid();
            }
        }
    }
}
