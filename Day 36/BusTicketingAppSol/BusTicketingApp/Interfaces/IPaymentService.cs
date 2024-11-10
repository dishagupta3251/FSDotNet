using BusTicketingApp.Models;

namespace BusTicketingApp.Interfaces
{
    public interface IPaymentService
    {
        public Task<string> AddPayment(Payment payment);
    }
}
