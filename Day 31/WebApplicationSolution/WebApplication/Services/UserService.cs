using System.Runtime.CompilerServices;
using AutoMapper;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class UserService:IUserService
    {
        private readonly IRepository<int,User> _repository;
        private readonly IMapper _mapper;
        public UserService(IRepository<int, User> respository,IMapper mapper) {
            _repository = respository;
            _mapper = mapper;
        }
        public async Task<User> Add(UserDTO user)
        {
            try
            {
                var newUser=_mapper.Map<User>(user);
                await _repository.Add(newUser);
                return newUser;
            }
            catch (Exception ex) { throw new Exception("Cannot add user"); }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                    var users=await _repository.GetAll();
                   return users;    
            }catch(Exception ex) { throw new Exception( ex.Message); }
        }

        
    }
}
