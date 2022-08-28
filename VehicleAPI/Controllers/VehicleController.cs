using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Model;
using VehicleAPI.Repository;
using VehicleAPI.Repository.IRepository;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            return await _vehicleRepository.GetVehicles();
        }
        
        [HttpGet("{id}")]
        public async Task<Vehicle>Get(int id)
        {
            return await _vehicleRepository.GetVehicleById(id); 
        }

        //post api
        [HttpPost]
        public async Task Post([FromBody]Vehicle vehicle)   
        {
           await _vehicleRepository.AddVehicles(vehicle);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Vehicle vehicle)
        {
        await _vehicleRepository.UpdateVehicles(vehicle);
        }

    //Delete api/<vehicle controller>
        [HttpDelete("{id}")]
        public async  Task Delete(int id) 
        {
            await _vehicleRepository.DeleteVehicles(id);
        }
    };
        
}

        
