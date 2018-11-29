using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Northwind.Clients.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };

            await GetProducts(client);
            await GetCategories(client);
        }

        private async static Task GetProducts(HttpClient client) {
            var response = await client.GetAsync("api/products?pageSize=5&pageNumber=2");
            System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private async static Task GetCategories(HttpClient client) {
            var response = await client.GetAsync("api/categories?pageSize=5&pageNumber=2");
            System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
