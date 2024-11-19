using BusTicketingApp.EmailModels;

namespace BusTicketingApp.EmailInterface
{
    public interface IEmailSender
    {

        public void SendEmail(Message email);
    }
}
