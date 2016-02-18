using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sales.WPFApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool VIP { get; set; }

        public static async Task<List<Client>> ToList()
        {
            HttpResponseMessage response = await APIService.GetClient().GetAsync("clients");
            string json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<Client>>(json);
        }

        public static async Task<HttpResponseMessage> Add(Client client)
        {
            string json = JsonConvert.SerializeObject(client);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await APIService.GetClient().PostAsync("clients", content);
        }

        public static async Task<HttpResponseMessage> Edit(Client client)
        {
            string json = JsonConvert.SerializeObject(client);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await APIService.GetClient().PutAsync($"clients/{client.Id}", content);
        }

        public static async Task<HttpResponseMessage> Delete(Client client)
        {
            return await APIService.GetClient().DeleteAsync($"clients/{client.Id}");
        }
    }
}
