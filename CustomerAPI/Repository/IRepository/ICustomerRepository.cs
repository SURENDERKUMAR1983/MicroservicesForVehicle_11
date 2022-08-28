using CustomerAPI.Models;

namespace CustomerAPI.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
    }
}
