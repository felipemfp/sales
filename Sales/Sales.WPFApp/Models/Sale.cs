using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sales.WPFApp.Models
{
    class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateSale { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }

        public Client Client { get; set; }
        public List<SaleProduct> SaleProducts { get; set; }

        public static async Task<Sale> Find(int id)
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync($"sales/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Sale>(json);
                }
            }
            throw new NullReferenceException("Product not found");
        }

        public static async Task<List<Sale>> ToList()
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync("sales");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Sale>>(json);
                }
            }
            return null;
        }

        public static async Task<HttpResponseMessage> Add(Sale sale)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(sale);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PostAsync($"sales", content);
            }
        }

        public static async Task<HttpResponseMessage> Edit(Sale sale)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(sale);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PutAsync($"sales/{sale.Id}", content);
            }
        }

        public static async Task<HttpResponseMessage> Delete(Sale sale)
        {
            using (var c = APIService.GetClient())
            {
                return await c.DeleteAsync($"sales/{sale.Id}");
            }
        }
    }
}
