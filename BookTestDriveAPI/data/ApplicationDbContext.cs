using BookTestDriveAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BookTestDriveAPI.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Reservation>Reservations  { get; set; }
    }
}
