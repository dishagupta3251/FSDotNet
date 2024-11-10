using BusTicketingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using System.Text;

namespace UnitTesting.RepositoryTest
{
    public class UserRepositoryTest
    {
        DbContextOptions options;
        TicketingContext context;
        UserRepository repository;
        [SetUp]
        public void Setup()
        {
            options= new DbContextOptionsBuilder<TicketingContext>()
              .UseInMemoryDatabase("TestRepo")
              .Options;
            context=new TicketingContext(options);
            repository=new UserRepository(context);
        }

        public async Task<User> AddDetails()
        {
            User user = new User()
            {
               
                FirstName = "test",
                Username = "test123",
                Email = "test@gmail.com",
                ContactNumber = "9372768901",
                Password = Encoding.UTF8.GetBytes("Password"),
                PasswordHash = Encoding.UTF8.GetBytes("TestPassword"),
                Role = Roles.Customer
            };
            return user;
        }

        [Test]
        public async Task TestAdd()
        {
           var user= await AddDetails();
            var result=await repository.Add(user);
            Assert.AreEqual(result.UserId, user.UserId);
        }
     
      
        [Test]
       

        public async Task TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));

        }
        [Test]
        public async Task TestDelete()
        {
            var user = await AddDetails();
            await repository.Add(user);
            var deletedUser = await  repository.Delete(user.Username);
            Assert.AreEqual(deletedUser.UserId, user.UserId);
        }
        [Test]
        public async Task TestDeleteException()
        { 
             Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(null));
        }
        [Test]
        public async Task TestGet()
        {
            var user = await AddDetails();
            await repository.Add(user);
            var _user= await repository.Get(user.Username);
            Assert.IsNotNull(_user.Username,user.Username);
        }
        [Test]
        public async Task TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get("hello"));
        }
        [Test]
        public async Task GetAllTest()
        {
            var user = await AddDetails();
            await repository.Add(user);
            var _user = await repository.GetAll();
            Assert.IsNotNull(_user);
        }
        [Test]
        public async Task GetAllTestException()
        {
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }
        [Test]
        public async Task UpdateTest()
        {
            var user = await AddDetails();
            await repository.Add(user);
            var _user=await repository.Update(user,user.Username);
            Assert.IsNotNull(_user);
        }
        [Test]
        public async Task UpdateTestException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(null,"hello"));
        }
    }
}
