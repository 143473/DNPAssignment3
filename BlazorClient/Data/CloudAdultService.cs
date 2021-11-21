using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Models;

namespace BlazorClient.Data
{
    public class CloudAdultService : IAdultData
    {
        private string uri = "https://localhost:5002";
        // private string uri = "http://jsonplaceholder.typicode.com";
        private readonly HttpClient client;

        public CloudAdultService() {
        
            client = new HttpClient();
        }

        public async Task<IList<Adult>> GetAdultsAsync() {
            var stringAsync = client.GetStringAsync(uri + "/adults");
            var message = await stringAsync;
            var adults = JsonSerializer.Deserialize<List<Adult>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return adults;
        }

        public async Task AddAdultAsync(Adult adult) {
            string adultAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri+"/Adult", content);
        }

        public async Task RemoveAdultAsync(int adultId) {
            await client.DeleteAsync($"{uri}/Adult/{adultId}");
        }

        public async Task UpdateAdultAsync(Adult adult) {
            string adultAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"{uri}/Adult/{adult.Id}", content);
        }

        public async Task<Adult> GetAdultAsync(int adultId)
        {
            var stringAsync = client.GetStringAsync(uri + $"/Adult/{adultId}");
            var message = await stringAsync;
            var adult = JsonSerializer.Deserialize<Adult>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return adult;
        }

       /* public Task<List<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2)
        {
            throw new System.NotImplementedException();
        }*/
    }
}