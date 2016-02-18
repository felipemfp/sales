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
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
            dataGrid.IsReadOnly = true;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            if (!String.IsNullOrEmpty(name))
            {
                Client client = new Client()
                {
                    Name = name
                };
                string json = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await APIService.GetClient().PostAsync("clients", content);
                InitDataGrid();
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Client client = dataGrid.SelectedItem as Client;
            string newName = textBoxName.Text.Trim();
            if (client != null && !String.IsNullOrEmpty(newName))
            {
                client.Name = newName;
                string json = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await APIService.GetClient().PutAsync($"clients/{client.Id}", content);
                InitDataGrid();
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Client client = dataGrid.SelectedItem as Client;
            if (client != null)
            {
                if (MessageBox.Show("Tem certeza?", "Delete Client", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await APIService.GetClient().DeleteAsync($"clients/{client.Id}");
                    InitDataGrid();
                }
            }
        }
    }
}
