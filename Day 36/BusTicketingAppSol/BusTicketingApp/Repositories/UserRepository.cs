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
        public string GenerateUsername(string fullname,string number)
        {
            string username = fullname + number.Substring(7);
            return username;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                entity.Username = GenerateUsername(entity.FirstName, entity.ContactNumber);
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
            existingUser.FirstName = entity.FirstName ?? existingUser.FirstName;
            existingUser.Email = entity.Email ?? existingUser.Email;
            existingUser.ContactNumber = entity.ContactNumber ?? existingUser.ContactNumber;
            existingUser.Password = entity.Password ?? existingUser.Password;
            existingUser.PasswordHash = entity.PasswordHash ?? existingUser.PasswordHash;
            existingUser.Role = entity.Role==existingUser.Role?existingUser.Role:entity.Role;
            if (entity.FirstName != existingUser.FirstName || entity.ContactNumber != existingUser.ContactNumber)
            {
                existingUser.Username = GenerateUsername(existingUser.FirstName, existingUser.ContactNumber);
            }

            _ticketingContext.Users.Update(existingUser);
            await _ticketingContext.SaveChangesAsync();

            return existingUser;
        }


    }
}
