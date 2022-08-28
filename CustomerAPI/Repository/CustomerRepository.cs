using Azure.Messaging.ServiceBus;
using CustomerAPI.data;
using CustomerAPI.Models;
using CustomerAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context; 
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            var vehicleInDb=await _context.Vehicles.FirstOrDefaultAsync(v=>v.Id==customer.VehicleId);
            if (vehicleInDb == null)
            {
                await _context.Vehicles.AddAsync(customer.Vehicle);
                await _context.SaveChangesAsync();
            }
            customer.Vehicle=null;
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            //Azure mail code
            string connectionString = "Endpoint=sb://vehiclenss.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n5KiLG9pEwqUEqiD0wl1y6jponrxfIsjTeYRQk3zBFk=";
            string queueName = "vehiclequeue";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);
            
            var JsonString=JsonConvert.SerializeObject(customer);
            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage(JsonString);

            // send the message
            await sender.SendMessageAsync(message);
        }
    }
}
