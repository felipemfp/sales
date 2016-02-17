using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sales.WPFApp
{
    class APIService
    {
        private static string _baseAddress = "http://localhost:62114/api/";

        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(_baseAddress);

            return client;
        }
    }
}
