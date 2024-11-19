using Moq;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Repositories;
using BusTicketingApp.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTicketingApp.Interfaces;

namespace UnitTesting.ServicesTest
{
    public class SeatServiceTest
    {
        private Mock<IRepository<Seats, int>> _mockSeatRepository;
        private Mock<ILogger<SeatService>> _mockLogger;
        private SeatService _seatService;

        [SetUp]
        public void Setup()
        {
            _mockSeatRepository = new Mock<IRepository<Seats, int>>();
            _mockLogger = new Mock<ILogger<SeatService>>();
            _seatService = new SeatService(_mockSeatRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetAllSeats_ShouldReturnSeats_WhenSeatsExist()
        {
            // Arrange
            var seats = new List<Seats>
            {
                new Seats { SeatsId = 1, SeatNumber =22, IsBooked = false, Price = 100 },
                new Seats { SeatsId = 2, SeatNumber =22, IsBooked = false, Price = 100 }
            };
            _mockSeatRepository.Setup(r => r.GetAll()).ReturnsAsync(seats);

            // Act
            var result = await _seatService.GetAllSeats();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(22, result.First().SeatNumber);
        }

        [Test]
        public async Task GetAllSeats_ShouldThrowException_WhenNoSeatsExist()
        {
            // Arrange
            _mockSeatRepository.Setup(r => r.GetAll()).ReturnsAsync(new List<Seats>());

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => _seatService.GetAllSeats());
            Assert.AreEqual("Cannot view Seats", exception.Message);
        }

        [Test]
        public async Task SeatById_ShouldReturnSeat_WhenSeatExists()
        {
            // Arrange
            var seat = new Seats { SeatsId = 1, SeatNumber = 11, IsBooked = false, Price = 100 };
            _mockSeatRepository.Setup(r => r.Get(1)).ReturnsAsync(seat);

            // Act
            var result = await _seatService.SeatById(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(11, result.SeatNumber);
        }

        [Test]
        public async Task SeatById_ShouldThrowException_WhenSeatDoesNotExist()
        {
            // Arrange
            _mockSeatRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Seats)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => _seatService.SeatById(1));
            Assert.AreEqual("Cannot get seat", exception.Message);
        }

        [Test]
        public async Task UpdateSeatStatus_ShouldReturnDTO_WhenSeatIsUpdated()
        {
            // Arrange
            var seat = new Seats { SeatsId = 1, SeatNumber = 11, IsBooked = false, Price = 100 };
            _mockSeatRepository.Setup(r => r.Get(1)).ReturnsAsync(seat);
            _mockSeatRepository.Setup(r => r.Update(It.IsAny<Seats>(), It.IsAny<int>())).ReturnsAsync(seat);

            // Act
            var result = await _seatService.UpdateSeatStatus(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(1, result.SeatId);
         
            Assert.AreEqual(100, result.Price);
        }

        [Test]
        public async Task UpdateSeatStatus_ShouldThrowException_WhenSeatIsAlreadyBooked()
        {
            // Arrange
            var seat = new Seats { SeatsId = 1, SeatNumber = 11, IsBooked = true, Price = 100 };
            _mockSeatRepository.Setup(r => r.Get(1)).ReturnsAsync(seat);

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _seatService.UpdateSeatStatus(1));
            Assert.AreEqual("Seat already booked.", exception.Message);
        }

        [Test]
        public async Task UpdateSeatStatus_ShouldThrowException_WhenSeatDoesNotExist()
        {
            // Arrange
            _mockSeatRepository.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Seats)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(() => _seatService.UpdateSeatStatus(1));
            Assert.AreEqual("Cannot update seat", exception.Message);
        }
    }
}
