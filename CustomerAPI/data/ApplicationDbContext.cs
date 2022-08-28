using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; } 
    }
       
}
