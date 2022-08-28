using BookTestDriveAPI.Model;
using BookTestDriveAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookTestDriveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            var reservation = await _reservationRepository.GetReservations();
            return reservation;
        }
        [HttpPut]
        public async Task Put(int id)
        {
            await _reservationRepository.UpdateMailStatus(id);
        }              
          

    }
}
