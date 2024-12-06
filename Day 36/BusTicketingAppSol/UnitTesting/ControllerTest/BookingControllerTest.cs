using BusTicketingApp.Controllers;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTesting.ControllersTest
{
    [TestFixture]
    public class BookingControllerTest
    {
        private Mock<IBookingService> _mockBookingService;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            _mockBookingService = new Mock<IBookingService>();
            _controller = new BookingController(_mockBookingService.Object);
        }

        [Test]
        public async Task GetBusesOnRoute_ShouldReturnOk_WhenBusesAreAvailable()
        {
            // Arrange
            var buses = new List<object> // Replace 'object' with actual Bus DTO or model
            {
                new { BusId = 1, BusNumber = "AB123", SeatsAvailable = 20 },
                new { BusId = 2, BusNumber = "CD456", SeatsAvailable = 15 }
            };
            _mockBookingService
                .Setup(s => s.GetAllBusesOnRoute("CityA", "CityB", It.IsAny<DateTime>(), 1, 10))
                .ReturnsAsync(buses);

            // Act
            var result = await _controller.GetBusesOnRoute("CityA", "CityB", DateTime.Now, 1, 10);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as IEnumerable<object>;
            Assert.NotNull(response);
            Assert.AreEqual(2, ((List<object>)response).Count);
        }

        [Test]
        public async Task GetBusDetailsWithSeats_ShouldReturnOk_WhenBusDetailsExist()
        {
            // Arrange
            var busDetails = new
            {
                BusId = 1,
                BusNumber = "AB123",
                SeatDetails = new List<object>
                {
                    new { SeatNumber = "A1", SeatType = "Window", IsBooked = false },
                    new { SeatNumber = "A2", SeatType = "Aisle", IsBooked = true }
                }
            };
            _mockBookingService
                .Setup(s => s.ViewBusDetailsAlongWithSeats(1))
                .ReturnsAsync(busDetails);

            // Act
            var result = await _controller.GetBusDetailsWithSeats(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual("A: Aisle W:Window", response.message);
        }

        [Test]
        public async Task Booking_ShouldReturnOk_WhenBookingIsSuccessful()
        {
            // Arrange
            var seatSelectionRequest = new SeatSelectionRequestDTO
            {
                BusId = 1,
                CustomerId = 101,
                Seats = new List<string> { "A1", "A2" }
            };
            var bookingConfirmationId = 12345;

            _mockBookingService
                .Setup(s => s.BookingConfirmation(seatSelectionRequest))
                .ReturnsAsync(bookingConfirmationId);

            // Act
            var result = await _controller.Booking(seatSelectionRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual(bookingConfirmationId, response.id);
        }

        [Test]
        public async Task InitiatePayment_ShouldReturnOk_WhenPaymentIsInitiatedSuccessfully()
        {
            // Arrange
            var paymentRequest = new PaymentRequestDTO
            {
                BookingId = 12345,
                PaymentMethod = "CreditCard",
                Amount = 500.0m
            };
            var paymentResponse = new { Status = "Pending", TransactionId = "TXN12345" };

            _mockBookingService
                .Setup(s => s.PaymentIntiation(paymentRequest))
                .ReturnsAsync(paymentResponse);

            // Act
            var result = await _controller.InitiatePayment(paymentRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual("Pending", response.Status);
        }

        [Test]
        public async Task GetBookingHistory_ShouldReturnOk_WhenHistoryExists()
        {
            // Arrange
            var bookingHistory = new List<object>
            {
                new { BookingId = 1, BusNumber = "AB123", BookingDate = DateTime.Now.AddDays(-1) },
                new { BookingId = 2, BusNumber = "CD456", BookingDate = DateTime.Now.AddDays(-2) }
            };

            _mockBookingService
                .Setup(s => s.BookingHistory(101))
                .ReturnsAsync(bookingHistory);

            // Act
            var result = await _controller.GetBookingHistory(101);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as IEnumerable<object>;
            Assert.NotNull(response);
            Assert.AreEqual(2, ((List<object>)response).Count);
        }
    }
}
