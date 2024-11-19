//using BusTicketingApp.Controllers;
//using BusTicketingApp.Interfaces;
//using BusTicketingApp.Models;
//using BusTicketingApp.Models.DTO;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace UnitTesting.ControllersTest
//{
//    [TestFixture]
//    public class UserControllerTest
//    {
//        private Mock<IUserServices> _mockUserServices;
//        private UserController _controller;

//        [SetUp]
//        public void Setup()
//        {
//            _mockUserServices = new Mock<IUserServices>();
//            _controller = new UserController(_mockUserServices.Object);
//        }

//        [Test]
//        public async Task Register_ShouldReturnOk_WhenUserIsRegisteredSuccessfully()
//        {
//            // Arrange
//            var userRegisterDTO = new UserRegisterDTO
//            {
//                FirstName = "Test",
//                Email = "test@test.com",
//                Password = "Password123",
//                ContactNumber = "1234567890",
//                Role = Roles.Customer
//            };
//            _mockUserServices.Setup(s => s.Register(userRegisterDTO)).ReturnsAsync("TestUser");

//            // Act
//            var result = await _controller.Register(userRegisterDTO);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);

//            var response = okResult.Value as dynamic;
//            Assert.NotNull(response);
//            Assert.AreEqual("Your username is given below", response.message);
//            Assert.AreEqual("TestUser", response.data);
//        }

//        [Test]
//        public void Register_ShouldThrowException_WhenModelStateIsInvalid()
//        {
//            // Arrange
//            _controller.ModelState.AddModelError("Error", "Invalid data");

//            // Act & Assert
//            Assert.ThrowsAsync<Exception>(async () => await _controller.Register(new UserRegisterDTO()));
//        }

//        [Test]
//        public async Task Login_ShouldReturnOk_WhenUserLogsInSuccessfully()
//        {
//            // Arrange
//            var loginRequest = new LoginRequestDTO
//            {
//                Username = "TestUser",
//                Password = "Password123"
//            };
//            var loginResponse = new LoginResponseDTO
//            {
//                Username = "TestUser",
//                Token = "TestToken",
//                UserId = 1
//            };
//            _mockUserServices.Setup(s => s.Login(loginRequest)).ReturnsAsync(loginResponse);

//            // Act
//            var result = await _controller.Login(loginRequest);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);

//            var response = okResult.Value as LoginResponseDTO;
//            Assert.NotNull(response);
//            Assert.AreEqual("TestUser", response.Username);
//            Assert.AreEqual("TestToken", response.Token);
//        }

//        [Test]
//        public async Task GetById_ShouldReturnOk_WhenUserIsFound()
//        {
//            // Arrange
//            var username = "TestUser";
//            var userProfile = new UserProfileDTO
//            {
//                Username = username,
//                FirstName = "Test",
//                Email = "test@test.com",
//                ContactNumber = "1234567890",
//                Role = Roles.Customer
//            };
//            _mockUserServices.Setup(s => s.GetById(username)).ReturnsAsync(userProfile);

//            // Act
//            var result = await _controller.Get(username);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);

//            var response = okResult.Value as UserProfileDTO;
//            Assert.NotNull(response);
//            Assert.AreEqual(username, response.Username);
//        }

//        [Test]
//        public async Task GetAll_ShouldReturnOk_WhenUsersAreFound()
//        {
//            // Arrange
//            var users = new List<UserProfileDTO>
//            {
//                new UserProfileDTO
//                {
//                    Username = "TestUser1",
//                    FirstName = "Test1",
//                    Email = "test1@test.com",
//                    ContactNumber = "1234567891",
//                    Role = Roles.Customer
//                },
//                new UserProfileDTO
//                {
//                    Username = "TestUser2",
//                    FirstName = "Test2",
//                    Email = "test2@test.com",
//                    ContactNumber = "1234567892",
//                    Role = Roles.Admin
//                }
//            };
//            _mockUserServices.Setup(s => s.GetAll()).ReturnsAsync(users);

//            // Act
//            var result = await _controller.GetAll();

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);

//            var response = okResult.Value as IEnumerable<UserProfileDTO>;
//            Assert.NotNull(response);
//            Assert.AreEqual(2, response.Count());
//        }

//        [Test]
//        public async Task UpdateUserPassword_ShouldReturnOk_WhenPasswordIsUpdated()
//        {
//            // Arrange
//            var username = "TestUser";
//            var password = "NewPassword123";
//            var operationStatus = new OperationStatusDTO
//            {
//                Username = username,
//                Status = "Update Successful"
//            };
//            _mockUserServices.Setup(s => s.UpdatePassword(username, password)).ReturnsAsync(operationStatus);

//            // Act
//            var result = await _controller.UpdateUserPassword(username, password);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.NotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);

//            var response = okResult.Value as OperationStatusDTO;
//            Assert.NotNull(response);
//            Assert.AreEqual(username, response.Username);
//            Assert.AreEqual("Update Successful", response.Status);
//        }
//    }
//}
