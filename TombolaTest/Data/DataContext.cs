using Microsoft.EntityFrameworkCore;
using TombolaTest.Models;

namespace TombolaTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }


        public DbSet<CoffeeBean> CofeeBeans { get; set; } 
    }
}
