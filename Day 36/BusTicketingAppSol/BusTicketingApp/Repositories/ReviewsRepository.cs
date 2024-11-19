using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class ReviewRepository : IRepository<Review, int>
    {
        private readonly TicketingContext _ticketingContext;

        public ReviewRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Review> Add(Review entity)
        {
            try
            {
                _ticketingContext.Reviews.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("Review");
            }
        }

        public async Task<Review> Delete(int key)
        {
            try
            {
                var reviewEntity = await Get(key);
                if (reviewEntity != null)
                {
                    _ticketingContext.Reviews.Remove(reviewEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return reviewEntity;
            }
            catch
            {
                throw new NotFoundException("Review");
            }
        }

        public async Task<Review> Get(int key)
        {
            try
            {
                var reviewEntity = await _ticketingContext.Reviews.FirstOrDefaultAsync(r => r.Id == key);
                if (reviewEntity == null) throw new Exception();
                return reviewEntity;
            }
            catch
            {
                throw new NotFoundException("Review");
            }
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            try
            {
                var reviews = await _ticketingContext.Reviews.ToListAsync();
                if (reviews.Count == 0) throw new Exception();
                return reviews;
            }
            catch
            {
                throw new CollectionEmptyException("Reviews");
            }
        }

        public async Task<Review> Update(Review entity, int key)
        {
            try
            {
                var existingReview = await Get(key);
                existingReview.Reviews = entity.Reviews ?? existingReview.Reviews;
                existingReview.OperatorId = entity.OperatorId;

               
                await _ticketingContext.SaveChangesAsync();

                return existingReview;
            }
            catch
            {
                throw new NotFoundException("Review");
            }
        }
    }
}
