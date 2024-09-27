using DoctorApplication.Interfaces;
using DoctorApplication.Models;

namespace DoctorApplication.Respositories
{
    public class UserRepository:IRepository<string,User>
    {
        List<User> _users=new List<User>
        {
            new User(){Username="disha123",Password="985940"},
            new User(){Username="niharika",Password="bon"}
        };

        public async Task<User> AddAsync(User entity)
        {
            _users.Add(entity);
            return entity;
        }

        public Task<User> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string username)
        {
            var user= _users.FirstOrDefault(u => u.Username == username);
            return  user;
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
