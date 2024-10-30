using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReportClaim.Contexts;
using ReportClaim.Exceptions;
using ReportClaim.Interfaces;
using ReportClaim.Models;

namespace ReportClaim.Repositories
{
    public class UserRepository : IRepository<User, string>
    {
        ReportClaimContext _context;
   
        public UserRepository(ReportClaimContext reportClaimContext)
        {
            _context = reportClaimContext;
            
        }
        public async Task<User> Create(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
               
                throw new CouldNotAddException("User");
            }
            
        }

        public async Task<User> Delete(string key)
        {

            var user = await GetById(key);
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                
                throw new Exception("Unable to delete");
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count == 0)
            {
                throw new CollectionEmptyException("Users");
            }
            return users;
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == id);
                return user;
            }
            catch (Exception e)
            {
               
                throw new CannotFindException("User");
            }
        }

        public async Task<User> Update(User entity)
        {
            var user = await GetById(entity.Username);
            if (user == null)
            {
                throw new CannotFindException("User");
            }
            try
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
           
                throw new Exception("Unable to modify user object");
            }
        }
    }
}
