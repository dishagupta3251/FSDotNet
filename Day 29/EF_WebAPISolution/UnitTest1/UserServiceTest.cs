
using EF_WebAPI.Models;
using EF_WebAPI.Models.DTO;
using EF_WebAPI.Repositories;
using EF_WebAPI.Services;
using EFCoreFirstAPI.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest1
{
    internal class UserServiceTest
    {
        DbContextOptions options;
        ShoppingContext context;
        UserService service;
        UserRepository repository;
        Mock<ILogger<UserService>> loggerUserService;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("TestService")
              .Options;
            context=new ShoppingContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();   
            repository = new UserRepository(context,loggerUserRepo.Object);
            loggerUserService = new Mock<ILogger<UserService>>();
            service = new UserService(repository,loggerUserService.Object); 

        }
        [Test]
        [TestCase("TestUsername","TestPassword",Roles.Admin)]
        public async Task TestAdd(string username, string password,Roles role)
        {
            var user = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = role

            };
            var userAdded=await service.Register(user);
            Assert.IsTrue(userAdded.Username.Equals(username));

        }
        [Test]
        public async Task TestAuthenticate()
        {
            var user = new UserCreateDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = Roles.Admin
            };
            var addedUser = await service.Register(user);
            var loggedInUser = await service.Autheticate(new LoginResponseDTO
            {
                Username = user.Username,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);


        }
    }
}
