using Microsoft.EntityFrameworkCore;
using SpeedDeal.DbModels;
using System.Net.Sockets;
using System.Xml.Linq;
namespace SpeedDeal
{
    public class AppDbContext : DbContext
    {
        public DbSet<TestModel> Test { get; set; }
        public DbSet<DealLink> Links { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Group> Groups { set; get; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp.db");
        }
    }

}
