using BusTicketingApp.Models;

namespace BusTicketingApp.Interfaces
{
    public interface IPaymentService
    {
        public Task<Payment> AddPayment(Payment payment);
    }
}
