using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class BusOperatorRepository : IRepository<BusOperator, int>
    {
        private readonly TicketingContext _ticketingContext;

        public BusOperatorRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<BusOperator> Add(BusOperator entity)
        {
            try
            {
                _ticketingContext.BusOperators.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("BusOperator");
            }
        }

        public async Task<BusOperator> Delete(int key)
        {
            try
            {
                var operatorEntity = await Get(key);
                if (operatorEntity != null)
                {
                    _ticketingContext.BusOperators.Remove(operatorEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return operatorEntity;
            }
            catch
            {
                throw new NotFoundException("BusOperator");
            }
        }

        public async Task<BusOperator> Get(int key)
        {
            try
            {
                var operatorEntity = await _ticketingContext.BusOperators.FirstOrDefaultAsync(o => o.OperatorId == key);
                if (operatorEntity == null) throw new Exception();
                return operatorEntity;
            }
            catch
            {
                throw new NotFoundException("BusOperator");
            }
        }

        public async Task<IEnumerable<BusOperator>> GetAll()
        {
            try
            {
                var operators = await _ticketingContext.BusOperators.ToListAsync();
                if (operators.Count == 0) throw new Exception();
                return operators;
            }
            catch
            {
                throw new CollectionEmptyException("BusOperators");
            }
        }

        public async Task<BusOperator> Update(BusOperator entity, int key)
        {
            try
            {
                var existingOperator = await Get(key);
                existingOperator.OperatorName = entity.OperatorName ?? existingOperator.OperatorName;
                existingOperator.OperatorContact = entity.OperatorContact ?? existingOperator.OperatorContact;

                _ticketingContext.BusOperators.Update(existingOperator);
                await _ticketingContext.SaveChangesAsync();

                return existingOperator;
            }
            catch
            {
                throw new NotFoundException("BusOperator");
            }
        }
    }
}
