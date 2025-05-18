using Microsoft.EntityFrameworkCore;
using TombolaTest.Models;

namespace TombolaTest.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDb)\MSSQLLocalDB;Database=CoffeeBeanDb;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<CoffeeBean> CoffeeBeans { get; set; }


    }
}