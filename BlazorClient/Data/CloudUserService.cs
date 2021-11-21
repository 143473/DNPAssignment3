using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorClient.Models;

namespace BlazorClient.Data
{
    public class CloudUserService : IUserService
    {
        private string uri = "https://localhost:5002";
        // private string uri = "http://jsonplaceholder.typicode.com";
        private readonly HttpClient client;

        public CloudUserService() {
        
            client = new HttpClient();
        }

        public async Task<IList<User>> GetUsersAsync() {
            Task<string> stringAsync = client.GetStringAsync(uri+"/users");
            string message = await stringAsync;
            List<User> result = JsonSerializer.Deserialize<List<User>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            foreach (var u in result)
            {
                Console.WriteLine(u.UserName);
            }
            return result;
        }

        public async Task AddUserAsync(User User) {
            string UserAsJson = JsonSerializer.Serialize(User);
            HttpContent content = new StringContent(UserAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PostAsync(uri+"/User", content);
        }

        public async Task RemoveUserAsync(int UserId) {
            await client.DeleteAsync($"{uri}/User/{UserId}");
        }

        public async Task UpdateUserAsync(User User) {
            string UserAsJson = JsonSerializer.Serialize(User);
            HttpContent content = new StringContent(UserAsJson,
                Encoding.UTF8,
                "application/json");
            await client.PatchAsync($"{uri}/User/{User.Id}", content);
        }

        public async Task<User> GetUserAsync(int UserId)
        {
            var stringAsync = await client.GetStringAsync($"{uri}/User/{UserId}");
            var user = JsonSerializer.Deserialize<User>(stringAsync, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return user;
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            try
            {
                var stringAsync = 
                    await client.GetStringAsync(uri + $"/User?userName={username}&password={password}");
                var user = JsonSerializer.Deserialize<User>(stringAsync, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
      
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}