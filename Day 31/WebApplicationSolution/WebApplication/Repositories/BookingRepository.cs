using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly EventBookingContext _eventBookingContext;
        public BookingRepository(EventBookingContext eventBookingContext)
        {
            _eventBookingContext = eventBookingContext;
        }
        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                _eventBookingContext.Bookings.Add(entity);
                await _eventBookingContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Booking> Delete(int key)
        {
            try
            {
                var user = await Get(key);
                _eventBookingContext.Bookings.Remove(user);
                await _eventBookingContext.SaveChangesAsync();
                return user;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Booking> Get(int key)
        {
            try
            {
                return await _eventBookingContext.Bookings.FirstOrDefaultAsync(u => u.Id == key);
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var book = await _eventBookingContext.Bookings.ToListAsync();
            if (book.Count < 0)
            {
                Console.WriteLine("No user found");
            }
            return book;
        }

        public Task<Booking> Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
