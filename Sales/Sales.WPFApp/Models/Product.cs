using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sales.WPFApp.Models
{
    class Product
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public static async Task<Product> Find(int id)
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync($"products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Product>(json);
                }
            }
            throw new NullReferenceException("Product not found");
        }

        public static async Task<List<Product>> TopSelling(int length = 10)
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync($"topselling?length={length}");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Product>>(json);
                }
            }
            return null;
        }

        public static async Task<List<Product>> ToList()
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync("products");
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Product>>(json);
                }
            }
            return null;
        }

        public static async Task<HttpResponseMessage> Add(Product product)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PostAsync("products", content);
            }
        }

        public static async Task<HttpResponseMessage> Edit(Product product)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PutAsync($"products/{product.Id}", content);
            }
        }

        public static async Task<HttpResponseMessage> Delete(Product product)
        {
            using (var c = APIService.GetClient())
            {
                return await c.DeleteAsync($"products/{product.Id}");
            }
        }
    }
}
