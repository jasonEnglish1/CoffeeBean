using Microsoft.EntityFrameworkCore;
using CoffeeBean.Models;

namespace CoffeeBean.Data
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

        public DbSet<CoffeeBeanDto> CoffeeBeans { get; set; }

    }
}