using VehicleAPI.Model;

namespace VehicleAPI.Repository.IRepository
{
    public interface IVehicleRepository 
    {
        Task<List<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicleById(int id);
        Task AddVehicles(Vehicle vehicle);
        Task UpdateVehicles(Vehicle vehicle);
        Task DeleteVehicles(int id);

    }
}
     