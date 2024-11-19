using System.Text;
using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class UserRepository : IRepository<User, string>
    {
        private readonly TicketingContext _ticketingContext;
        public UserRepository(TicketingContext ticketingContext) {
            _ticketingContext = ticketingContext;
        }
        
        public async Task<User> Add(User entity)
        {
            try
            {
                _ticketingContext.Users.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch {
                throw new CouldNotAddException("User");
            }
           
        }

        public async Task<User> Delete(string key)
        {
            try
            {
                var user = await Get(key);
                if (user != null)
                {
                    _ticketingContext.Users.Remove(user);
                    await _ticketingContext.SaveChangesAsync();
                   
                }
                return user;
            }
            catch { throw new NotFoundException("User"); }
           
        }

  

        public async Task<User> Get(string key)
        {
            try
            {
                var user=await _ticketingContext.Users.FirstOrDefaultAsync(u=>u.Username==key);
                if (user == null) throw new Exception();
                return user;
            }
            catch
            {
                throw new NotFoundException("User");
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users=await _ticketingContext.Users.ToListAsync();
                if (users.Count == 0) throw new Exception();
                return users;
            }
            catch
            {
                throw new CollectionEmptyException("Users");
            }
        }



        
        public async Task<User> Update(User entity, string key)
        {
            var existingUser = await Get(key);
           
          
            existingUser.Password = entity.Password ?? existingUser.Password;
          

            
            await _ticketingContext.SaveChangesAsync();

            return existingUser;
        }


    }
}
