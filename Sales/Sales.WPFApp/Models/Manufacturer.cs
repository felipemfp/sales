﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sales.WPFApp.Models
{
    class Manufacturer
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static async Task<List<Manufacturer>> ToList()
        {
            using (var c = APIService.GetClient())
            {
                HttpResponseMessage response = await c.GetAsync("manufacturers");
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Manufacturer>>(json);
            }
        }

        public static async Task<HttpResponseMessage> Add(Manufacturer manufacturer)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(manufacturer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PostAsync("manufacturers", content);
            }
        }

        public static async Task<HttpResponseMessage> Edit(Manufacturer manufacturer)
        {
            using (var c = APIService.GetClient())
            {
                string json = JsonConvert.SerializeObject(manufacturer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                return await c.PutAsync($"manufacturers/{manufacturer.Id}", content);
            }
        }

        public static async Task<HttpResponseMessage> Delete(Manufacturer manufacturer)
        {
            using (var c = APIService.GetClient())
            {
                return await c.DeleteAsync($"manufacturers/{manufacturer.Id}");
            }
        }
    }
}
