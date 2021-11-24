using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace FileData
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Users.db");
        }
        
    }
}