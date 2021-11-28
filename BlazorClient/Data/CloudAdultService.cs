using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorClient.Data
{
    public class CloudAdultService : IAdultData
    {
        private string uri = "https://localhost:5002";
        private readonly HttpClient client;

        public CloudAdultService() {
        
            client = new HttpClient();
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

       public async Task<IList<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2)
       {
           var filters =
               $"?searchByName={searchByName}&filter={filter}&filter2={filter2}";
            var stringAsync = client.GetStringAsync(uri + $"/FilteredAdults"+filters);
            var adultList = await stringAsync;
            var adults = JsonSerializer.Deserialize<IList<Adult>>(adultList, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return adults;
        }

       public async Task<List<string>> GetFilterCategories()
       {
           var stringAsync = client.GetStringAsync(uri + $"/Adult/categories");
           var categories1 = await stringAsync;
           var categories = JsonSerializer.Deserialize<List<string>>(categories1, new JsonSerializerOptions
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase
           });
           return categories;
       }
       
       public async Task<List<string>> GetFilterList(string category)
       {
           var stringAsync = client.GetStringAsync(uri + $"/Adult/filterList?category={category}");
           var filters = await stringAsync;
           var filters1 = JsonSerializer.Deserialize<List<string>>(filters, new JsonSerializerOptions
           {
               PropertyNamingPolicy = JsonNamingPolicy.CamelCase
           });
           return filters1;
       }
    }
}