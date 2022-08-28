using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VehicleAPI.data;
using VehicleAPI.Model;
using VehicleAPI.Repository.IRepository;

namespace VehicleAPI.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddVehicles(Vehicle vehicle)
        {
            try
            {
                await _context.Vehicles.AddAsync(vehicle);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Debug.Write(vehicle.Displacement);
            }
        }

        public async Task DeleteVehicles(int id)
        {
            var VehiclesInDb = await _context.Vehicles.FindAsync(id);
            if (VehiclesInDb != null)
                _context.Vehicles.Remove(VehiclesInDb);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vehicle>> GetVehicles() 
        {
           return await _context.Vehicles.ToListAsync();
        }

        public async Task UpdateVehicles(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
