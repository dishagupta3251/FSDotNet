using BusTicketingApp.Controllers;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
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
    public class ReviewControllerTest
    {
        private Mock<IReviewService> _mockReviewService;
        private ReviewController _controller;

        [SetUp]
        public void Setup()
        {
            _mockReviewService = new Mock<IReviewService>();
            _controller = new ReviewController(_mockReviewService.Object);
        }

        [Test]
        public async Task PostReview_ShouldReturnOk_WhenReviewIsPostedSuccessfully()
        {
            // Arrange
            var reviewRequestDTO = new ReviewRequestDTO
            {
                OperatorId = 1,
                Reviews = "Great service!"
            };

            var expectedReview = new Review
            {
                Id = 1,
                OperatorId = 1,
                Reviews = "Great service!"
            };

            _mockReviewService.Setup(s => s.Post(reviewRequestDTO)).ReturnsAsync(expectedReview);

            // Act
            var result = await _controller.PostReview(reviewRequestDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as Review;
            Assert.NotNull(response);
            Assert.AreEqual(1, response.Id);
            Assert.AreEqual("Great service!", response.Reviews);
        }

        [Test]
        public async Task PostReview_ShouldReturnBadRequest_WhenReviewCannotBePosted()
        {
            // Arrange
            var reviewRequestDTO = new ReviewRequestDTO
            {
                OperatorId = 1,
                Reviews = "Poor service!"
            };

            _mockReviewService.Setup(s => s.Post(reviewRequestDTO)).ReturnsAsync((Review)null);

            // Act
            var result = await _controller.PostReview(reviewRequestDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var response = badRequestResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual("Review not added", response.message);
        }

        [Test]
        public async Task GetReview_ShouldReturnOk_WhenReviewsAreAvailable()
        {
            // Arrange
            var operatorId = 1;
            var reviews = new List<Review>
            {
                new Review { Id = 1, OperatorId = operatorId, Reviews = "Excellent service!" },
                new Review { Id = 2, OperatorId = operatorId, Reviews = "Average experience." }
            };

            _mockReviewService.Setup(s => s.GetAllReviewsByOperatorId(operatorId)).ReturnsAsync(reviews);

            // Act
            var result = await _controller.GetReview(operatorId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as IEnumerable<Review>;
            Assert.NotNull(response);
            Assert.AreEqual(2, response.Count());
        }

        [Test]
        public async Task GetReview_ShouldReturnNotFound_WhenNoReviewsExist()
        {
            // Arrange
            var operatorId = 99;

            _mockReviewService.Setup(s => s.GetAllReviewsByOperatorId(operatorId)).ReturnsAsync(new List<Review>());

            // Act
            var result = await _controller.GetReview(operatorId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);

            var response = notFoundResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual("Cannot find reviews for this operator.", response.message);
        }
    }
}
