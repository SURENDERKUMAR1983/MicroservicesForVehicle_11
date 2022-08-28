using Microsoft.EntityFrameworkCore;
using VehicleAPI.Model;

namespace VehicleAPI.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; } 
    }
}
