using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusTicketingApp.Repositories
{
    public class BusOperatorRepository : IRepository<BusOperator, int>
    {
        private readonly TicketingContext _ticketingContext;
        private readonly ILogger<BusOperatorRepository> _logger;

        public BusOperatorRepository(TicketingContext ticketingContext, ILogger<BusOperatorRepository> logger)
        {
            _ticketingContext = ticketingContext;
            _logger = logger;
        }

        public async Task<BusOperator> Add(BusOperator entity)
        {
            try
            {
                _ticketingContext.BusOperators.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                _logger.LogInformation("Added a new BusOperator with ID {OperatorId}.", entity.OperatorId);
                return entity;
            }
            catch
            {
                _logger.LogError("Failed to add a new BusOperator.");
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
                    _logger.LogInformation("Deleted BusOperator with ID {OperatorId}.", key);
                }
                return operatorEntity;
            }
            catch
            {
                _logger.LogError("Failed to delete BusOperator with ID {OperatorId}.", key);
                throw new NotFoundException("BusOperator");
            }
        }

        public async Task<BusOperator> Get(int key)
        {
            try
            {
                var operatorEntity = await _ticketingContext.BusOperators.FirstOrDefaultAsync(o => o.OperatorId == key);
                if (operatorEntity == null) throw new Exception();
                _logger.LogInformation("Retrieved BusOperator with ID {OperatorId}.", key);
                return operatorEntity;
            }
            catch
            {
                _logger.LogError("BusOperator with ID {OperatorId} not found.", key);
                throw new NotFoundException("BusOperator");
            }
        }

        public async Task<IEnumerable<BusOperator>> GetAll()
        {
            try
            {
                var operators = await _ticketingContext.BusOperators.ToListAsync();
                if (operators.Count == 0) throw new Exception();
                _logger.LogInformation("Retrieved all BusOperators. Count: {Count}.", operators.Count);
                return operators;
            }
            catch
            {
                _logger.LogError("No BusOperators found.");
                throw new CollectionEmptyException("BusOperators");
            }
        }

        public async Task<BusOperator> Update(BusOperator entity, int key)
        {
            try
            {
                var existingOperator = await Get(key);
             

                if (!string.IsNullOrWhiteSpace(entity.Email))
                {
                    existingOperator.Email = entity.Email;
                }

                if (!string.IsNullOrWhiteSpace(entity.OperatorContact))
                {
                    existingOperator.OperatorContact = entity.OperatorContact;
                }

                if (!string.IsNullOrWhiteSpace(entity.LicenseNumber))
                {
                    existingOperator.LicenseNumber = entity.LicenseNumber;
                }
                
                if (!string.IsNullOrWhiteSpace(entity.CompanyName))
                {
                    existingOperator.CompanyName = entity.CompanyName;
                }

                await _ticketingContext.SaveChangesAsync();
                _logger.LogInformation("Updated BusOperator with ID {OperatorId}.", key);
                return existingOperator;
            }
            catch
            {
                _logger.LogError("Failed to update BusOperator with ID {OperatorId}.", key);
                throw new NotFoundException("BusOperator");
            }
        }
    }
}
