using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly EventBookingContext _eventBookingContext;
        public UserRepository(EventBookingContext eventBookingContext) {
            _eventBookingContext = eventBookingContext;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                _eventBookingContext.Users.Add(entity);
                await _eventBookingContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<User> Delete(int key)
        {
            try
            {
                var user = await Get(key);
                _eventBookingContext.Users.Remove(user);
                await _eventBookingContext.SaveChangesAsync();
                return user;


            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Get(int key)
        {
            try { 
                return await _eventBookingContext.Users.FirstOrDefaultAsync(u=> u.Id==key);  
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _eventBookingContext.Users.ToListAsync() ;
            if(users.Count<0)
            {
                Console.WriteLine("No user found");
            }
            return users ;

        }

        public async Task<User> Update(User entity)
        {
            var oldUser = await Get(entity.Id);
            if (oldUser != null) {
                oldUser.Name = entity.Name;
                oldUser.Email = entity.Email;
                await _eventBookingContext.SaveChangesAsync();
                 }
            else
            {
                Console.WriteLine("No user found");
            }
            return entity;

        }

      
    }
}
