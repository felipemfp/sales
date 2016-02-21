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
    public partial class ClientsWindow : Window
    {
        public ClientsWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void ClearFields()
        {
            textBoxName.Text = string.Empty;
        }

        private async void InitDataGrid()
        {
            dataGrid.ItemsSource = await Client.ToList();
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
                HttpResponseMessage response = await Client.Add(client);
                if (response.IsSuccessStatusCode)
                {
                    InitDataGrid();
                    ClearFields();
                    MessageBox.Show($"Client {client.Name} was added");
                }
                else
                {
                    MessageBox.Show($"Client {client.Name} wasn't added");
                }
            }
            else
            {
                MessageBox.Show("Enter a name...");
            }
        }

        private async void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)dataGrid.SelectedItem;
            if (client != null)
            {
                string newName = textBoxName.Text.Trim();
                if (!String.IsNullOrEmpty(newName))
                {
                    client.Name = newName;
                    HttpResponseMessage response = await Client.Edit(client);
                    if (response.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFields();
                        MessageBox.Show($"Client {client.Name} was edited");
                    }
                    else
                    {
                        MessageBox.Show($"Client {client.Name} wasn't edited");
                    }
                }
                else
                {
                    MessageBox.Show("Name is required...");
                }
            }
            else
            {
                MessageBox.Show("Select a client...");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)dataGrid.SelectedItem;
            if (client != null)
            {
                if (MessageBox.Show("Are you sure?", "Delete Client", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HttpResponseMessage response = await Client.Delete(client);
                    if (response.IsSuccessStatusCode)
                    {
                        InitDataGrid();
                        ClearFields();
                        MessageBox.Show($"Client {client.Name} was deleted");
                    }
                    else
                    {
                        MessageBox.Show($"Client {client.Name} wasn't deleted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a client...");
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client = (Client)dataGrid.SelectedItem;
            if (client != null)
            {
                textBoxName.Text = client.Name;
            }
        }
    }
}
