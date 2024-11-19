using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class PaymentRepository : IRepository<Payment, int>
    {
        private readonly TicketingContext _ticketingContext;

        public PaymentRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Payment> Add(Payment entity)
        {
            try
            {
                _ticketingContext.Payments.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("Payment");
            }
        }

        public async Task<Payment> Delete(int key)
        {
            try
            {
                var paymentEntity = await Get(key);
                if (paymentEntity != null)
                {
                    _ticketingContext.Payments.Remove(paymentEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return paymentEntity;
            }
            catch
            {
                throw new NotFoundException("Payment");
            }
        }

        public async Task<Payment> Get(int key)
        {
            try
            {
                var paymentEntity = await _ticketingContext.Payments
                    .FirstOrDefaultAsync(p => p.Id == key);

                if (paymentEntity == null) throw new Exception();
                return paymentEntity;
            }
            catch
            {
                throw new NotFoundException("Payment");
            }
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            try
            {
                var payments = await _ticketingContext.Payments.ToListAsync();

                if (payments.Count == 0) throw new Exception();
                return payments;
            }
            catch
            {
                throw new CollectionEmptyException("Payments");
            }
        }

        public async Task<Payment> Update(Payment entity, int key)
        {
            try
            {
                var existingPayment = await Get(key);

                existingPayment.Type = entity.Type;
                

                
                await _ticketingContext.SaveChangesAsync();

                return existingPayment;
            }
            catch
            {
                throw new NotFoundException("Payment");
            }
        }
    }
}
