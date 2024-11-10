
using System.Text;
using AutoMapper;
using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Repositories;
using BusTicketingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTesting.ServicesTest
{
    public  class UserServiceTest
    {
        DbContextOptions options;
        TicketingContext context;
        UserRepository userRepository;
        UserService service;
        Mock<TokenService> mockTokenService;
        Mock<IMapper> mockMapper;
        Mock<IConfiguration> mockConfiguration;


        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
             .UseInMemoryDatabase("TestService")
             .Options;
            context = new TicketingContext(options);
            userRepository=new UserRepository(context);
            mockMapper = new Mock<IMapper>();
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");
            service = new UserService(userRepository, mockTokenService.Object,mockMapper.Object);
        }
        [Test]
        public async Task RegisterTest()
        {
            UserRegisterDTO user = new UserRegisterDTO()
            {

                FirstName = "test",
                Password="test123",
                Email = "test@gmail.com",
                ContactNumber = "9372768901",
                Role = Roles.Customer
            };
            var result=await service.Register(user);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task RegisterTestException()
        {
            Assert.ThrowsAsync<Exception>(async () => await service.Register(null));

        }
        [Test]
        public async Task LoginTest()
        {
            UserRegisterDTO user = new UserRegisterDTO()
            {

                FirstName = "test",
                Password = "test123",
                Email = "test@gmail.com",
                ContactNumber = "9372768901",
                Role = Roles.Customer
            };
            var _user=await service.Register(user);
            LoginRequestDTO loginRequest = new LoginRequestDTO()
            {
                Username = "test901",
                Password = "test123"
            };
            
            var result= await service.Login(loginRequest);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteTest()
        {
            UserRegisterDTO user = new UserRegisterDTO()
            {

                FirstName = "test",
                Password = "test123",
                Email = "test@gmail.com",
                ContactNumber = "9372768901",
                Role = Roles.Customer
            };
            var _user=await service.Register(user);
            var result = await service.Delete("test901");
            Assert.IsNull(_user);
        }
        [Test]
        public async Task GetAllTest()
        {
            UserRegisterDTO user = new UserRegisterDTO()
            {

                FirstName = "test",
                Password = "test123",
                Email = "test@gmail.com",
                ContactNumber = "9372768901",
                Role = Roles.Customer
            };
            var result = await service.Register(user);
            var _user=await service.GetAll();
            Assert.AreEqual(_user.Count(), 1);


        }
        [Test]
        public async Task GetAllTestException()
        {
            Assert.ThrowsAsync<Exception>(async () => await service.GetAll());
        }
    }
}
