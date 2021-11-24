using System.Xml;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace FileData
{
    public class AdultsContext : DbContext
    {
        public DbSet<Adult> Adults { get; set; }
        
        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Adults.db");
        }
    }
}