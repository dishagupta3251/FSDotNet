using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;

namespace BusTicketingApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment,int> _paymentRepository;
        public PaymentService(IRepository<Payment,int> repository)
        {
            _paymentRepository = repository;
        }
        public async Task<Payment> AddPayment(Payment payment)
        {
            try
            {
                
                var paymentAdded=await _paymentRepository.Add(payment);
                if (paymentAdded == null)
                {
                    throw new Exception("Payment Failed");
                }
                return paymentAdded ;
            }
            catch 
            {
                throw new Exception();
            }
           
        }
    }
}
