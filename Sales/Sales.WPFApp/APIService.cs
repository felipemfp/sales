using System;
using System.Net.Http;

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
