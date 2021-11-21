using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;


namespace WebAPI.Data
{
    public class UserService : IUserService
    {
        private static IList<User> users;

        public UserService()
        {
            users = new List<User>();
            Seed();
        }

        public async Task<IList<User>>  GetUsersAsync()
        {
            return users;
        }

        public async Task AddUserAsync(User newUser)
        {
            users.Add(new User()
            {
                Id = users.Max(user => user.Id)+1,
                Password = newUser.Password,
                Role = "user",
                SecurityLevel = 1,
                UserName = newUser.UserName
            });
        }

        public async Task RemoveUserAsync(int? userId)
        {
            User user = users.First(t => t.Id == userId);
            users.Remove(user);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            User toUpdate = users.First(t => t.Id == user.Id);
            users.Remove(toUpdate);
            users.Add(user);
            return user;
        }

        public async Task<User> GetUserAsync(int? id)
        {
            return users.FirstOrDefault(t => t.Id == id);
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User user = users.FirstOrDefault(u => u.UserName.Equals(userName) && u.Password.Equals(password));
            if (user != null)
            {
                return user;
            } 
            throw new Exception("User not found");
        }

        private void Seed()
        {
            users.Add(new User
            {
                Id = 0,
                UserName = "admin",
                Password = "1234",
                Role = "admin",
                SecurityLevel = 3
            });
            users.Add(new User
            {
                Id = 1,
                UserName = "claudiu",
                Password = "1234",
                Role = "user",
                SecurityLevel = 1
            });
            users.Add(new User
            {
                Id = 2,
                UserName = "emanuel",
                Password = "1234",
                Role = "moderator",
                SecurityLevel = 2
            });
        }
    }
}