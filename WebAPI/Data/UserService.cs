using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileData;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;


namespace WebAPI.Data
{
    public class UserService : IUserService
    {
        //private static IList<User> users;
        private AdultsContext adultsContext;

        public UserService(AdultsContext adultsContext)
        {
            this.adultsContext = adultsContext;
            //users = new List<User>();
            Seed();
        }

        public async Task<IList<User>>  GetUsersAsync()
        {
            //return users;
            return await adultsContext.Users.ToListAsync();
        }

        public async Task AddUserAsync(User newUser)
        {
            adultsContext.Users.Add(new User()
            {
                Id = adultsContext.Users.Max(user => user.Id)+1,
                Password = newUser.Password,
                Role = "user",
                SecurityLevel = 1,
                UserName = newUser.UserName
            });
        }

        public async Task RemoveUserAsync(int? userId)
        {
            User toDelete = await adultsContext.Users.FirstOrDefaultAsync(t => t.Id == userId);
            if (toDelete != null)
            {
                adultsContext.Users.Remove(toDelete);
                await adultsContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                User toUpdate = await adultsContext.Users.FirstAsync(t => t.Id == user.Id);
                if (toUpdate != null)
                {
                      adultsContext.Users.Update(user);
                      await adultsContext.SaveChangesAsync();
                }
              
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find adult with id {user.Id}");
            }
            
        }

        public async Task<User> GetUserAsync(int? id)
        {
            return await adultsContext.Users.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            User user = await adultsContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName) && u.Password.Equals(password));
            if (user != null)
            {
                return user;
            } 
            throw new Exception("User not found");
        }

        private async Task Seed()
        {
            adultsContext.Users.Add(new User
            {
                Id = 1,
                UserName = "claudiu",
                Password = "1234",
                Role = "user",
                SecurityLevel = 1
            });
            adultsContext.Users.Add(new User
            {
                Id = 2,
                UserName = "emanuel",
                Password = "1234",
                Role = "moderator",
                SecurityLevel = 2
            });
            adultsContext.Users.Add(new User
            {
                Id = 0,
                UserName = "admin",
                Password = "1234",
                Role = "admin",
                SecurityLevel = 3
            });
            await adultsContext.SaveChangesAsync();
        }
    }
}