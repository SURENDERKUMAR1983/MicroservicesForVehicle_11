using Azure.Messaging.ServiceBus;
using BookTestDriveAPI.data;
using BookTestDriveAPI.Model;
using BookTestDriveAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace BookTestDriveAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

           public async Task<List<Reservation>> GetReservations()
            {
                  //Azure mail code
                 string connectionString = "Endpoint=sb://vehiclenss.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n5KiLG9pEwqUEqiD0wl1y6jponrxfIsjTeYRQk3zBFk=";
                 string queueName = "vehiclequeue";
                await using var client = new ServiceBusClient(connectionString);
                // create a receiver that we can use to receive the message
                ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            //ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(100);
                if (receivedMessages == null)
                {
                   return null;
                }
                foreach (var receivedMessage in receivedMessages)
                {
                    string body = receivedMessage.Body.ToString();
                    var messagecreate=JsonConvert.DeserializeObject<Reservation>(body);
                    await _context.Reservations.AddAsync(messagecreate);                   ;
                    await _context.SaveChangesAsync();
                    await receiver.CompleteMessageAsync(receivedMessage);
                }
                // get the message body as a string

                return await _context.Reservations.ToListAsync();

           }
        public async Task UpdateMailStatus(int id)
        {
            var reservationInDb = await _context.Reservations.FindAsync(id);
            if (reservationInDb == null && reservationInDb.IsMailSent == false)
            {
                var smtpClient = new SmtpClient("smtp.mail.outlook.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("surenderks089@outlook.com", "abc"),
                    EnableSsl = true
                };
                smtpClient.Send("surenderks089@outlook.com", reservationInDb.Email, "Vehicle Test drive", "hello message");
                await _context.SaveChangesAsync();
            }
        }

    }

       

       
    
}
