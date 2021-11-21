using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorClient.Models;


namespace BlazorClient.Data
{
    public interface IUserService
    {
        Task<IList<User>> GetUsersAsync();
        Task AddUserAsync(User newUser);
        Task RemoveUserAsync(int userId);
        Task UpdateUserAsync(User user);
        Task<User> GetUserAsync(int id);
        Task<User> ValidateUserAsync(string username, string password);
    }
}