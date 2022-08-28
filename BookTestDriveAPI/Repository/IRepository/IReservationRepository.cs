using BookTestDriveAPI.Model;

namespace BookTestDriveAPI.Repository.IRepository
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetReservations();
        Task UpdateMailStatus(int id);
    }
}
