using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using BusTicketingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTesting.RepositoryTest
{
    public class ReviewRepositoryTest
    {
        private DbContextOptions<TicketingContext> _options;
        private TicketingContext _context;
        private ReviewRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestReviewRepo")
                .Options;

            _context = new TicketingContext(_options);
            _repository = new ReviewRepository(_context);
        }

        private Review CreateReview()
        {
           

            var busOperator = new BusOperator
            {
                OperatorId = 1,
                OperatorName = "Operator1",
                LicenseNumber = "ABC123",
                CompanyName = "Bus Company",
                Email = "operator1@example.com",
                OperatorContact = "1234567890",
                UserId = 1
            };
            _context.BusOperators.Add(busOperator);
            _context.SaveChanges();

            return new Review
            {
                Id = 1,
                OperatorId = busOperator.OperatorId,
                Reviews = "Good service",
                Operator = busOperator
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var review = CreateReview();
            var result = await _repository.Add(review);

            Assert.AreEqual(review.Id, result.Id);
            Assert.AreEqual(review.Reviews, result.Reviews);
            Assert.AreEqual(review.OperatorId, result.OperatorId);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var review = CreateReview();
            var addedReview = await _repository.Add(review);

            var deletedReview = await _repository.Delete(addedReview.Id);

            Assert.AreEqual(deletedReview.Id, addedReview.Id);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(0));
        }

        [Test]
        public async Task TestGet()
        {
            var review = CreateReview();
            var addedReview = await _repository.Add(review);

            var fetchedReview = await _repository.Get(addedReview.Id);

            Assert.AreEqual(fetchedReview.Id, addedReview.Id);
            Assert.AreEqual(fetchedReview.Reviews, addedReview.Reviews);
            Assert.AreEqual(fetchedReview.OperatorId, addedReview.OperatorId);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task TestGetAll()
        {
            _context.Reviews.RemoveRange(_context.Reviews); // Ensure no data in the context.
            await _context.SaveChangesAsync();

            var review = CreateReview();
            await _repository.Add(review);

            var reviews = await _repository.GetAll();

            Assert.IsNotEmpty(reviews);
            Assert.AreEqual(reviews.Count(), 1);
        }

        [Test]
        public void TestGetAllException()
        {
            _context.Reviews.RemoveRange(_context.Reviews); // Ensure no data in the context.
            _context.SaveChanges();
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        public async Task TestUpdate()
        {
            var review = CreateReview();
            var addedReview = await _repository.Add(review);

            addedReview.Reviews = "Excellent service";

            var updatedReview = await _repository.Update(addedReview, addedReview.Id);

            Assert.AreEqual(updatedReview.Reviews, "Excellent service");
        }

        [Test]
        public void TestUpdateException()
        {
            var invalidReview = new Review
            {
                Id = 999,
                Reviews = "Invalid update"
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(invalidReview, invalidReview.Id));
        }
    }
}
