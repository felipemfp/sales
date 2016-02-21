using System;
using System.Collections.Generic;
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

        public static async Task<Client> Find(int id)
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync($"clients/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Client>(json);
                }
            }
            throw new NullReferenceException("Client not found");
        }

        public static async Task<List<Client>> Vips()
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync("vips");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Client>>(json);
                }
            }
            return null;
        }

        public static async Task<List<Client>> ToList()
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync("clients");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Client>>(json);
                }
            }
            return null;
        }

        public static async Task<HttpResponseMessage> Add(Client client)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PostAsync("clients", content);
            }
        }

        public static async Task<HttpResponseMessage> Edit(Client client)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(client);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PutAsync($"clients/{client.Id}", content);
            }
        }

        public static async Task<HttpResponseMessage> Delete(Client client)
        {
            using (var c = APIService.GetClient())
            {
                return await c.DeleteAsync($"clients/{client.Id}");
            }
        }
    }
}
