using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_WebAPI.Controllers;
using EF_WebAPI.Models;
using EF_WebAPI.Models.DTO;
using EF_WebAPI.Repositories;
using EF_WebAPI.Services;
using EFCoreFirstAPI.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest1
{ 
    internal class UserControllerTest
    {
        DbContextOptions options;
        Mock<ILogger<UserService>> loggerService;
        Mock<ILogger<UserController>> loggerController;
        Mock<ILogger<UserRepository>> loggerRepo;
        ShoppingContext dbContext;
        UserRepository userRepository;
        UserController controller;
        UserService userService;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
           .UseInMemoryDatabase("TestAdd")
             .Options;
            dbContext=new ShoppingContext(options);
            
            loggerService = new Mock<ILogger<UserService>>();
            loggerController = new Mock<ILogger<UserController>>();
            loggerRepo = new Mock<ILogger<UserRepository>>();
            userRepository = new UserRepository(dbContext, loggerRepo.Object);
            userService=new UserService(userRepository,loggerService.Object);
            controller = new UserController(userService,loggerController.Object);
           
            
        }
        [Test]
        public async Task TestRegister()
        {
            UserCreateDTO user = new UserCreateDTO()
            {
                Username="testUsername",
                Password="iss",
                Role=Roles.Admin,

            };
            var loginResponse= await controller.Register(user);
            Assert.IsNotNull(loginResponse);
            var resultObject = loginResponse.Result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);

        }
        [Test]
        [TestCase("disha","gupta",Roles.Admin)]
        public async Task TestLogin(string username, string password,Roles role)
        {

            UserCreateDTO user = new UserCreateDTO()
            {
                Username = username,
                Password = password,
                Role = Roles.Admin,

            };
            var loginResponse = await controller.Register(user);
            Assert.IsNotNull(loginResponse);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Username = username,
                Password = password,
            };
            var response= await controller.Login(loginResponseDTO);
            Assert.IsNotNull(response);
            var resultObject = response.Result as OkObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

    }
}
