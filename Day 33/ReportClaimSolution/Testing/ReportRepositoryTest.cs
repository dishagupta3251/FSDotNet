using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReportClaim.Contexts;
using ReportClaim.Exceptions;
using ReportClaim.Models;
using ReportClaim.Repositories;
using System.Threading.Tasks;

namespace ReportClaim.Tests
{
    public class ReportRepoTest
    {
        private ReportClaimContext _context;
        private ReportRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ReportClaimContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReportDb")
                .Options;

            _context = new ReportClaimContext(options);
            _repo = new ReportRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Add()
        {
            var report = new Report
            {
                Id = 1,
                ClaimaintName = "John Doe",
                ClaimaintPhone = "1234567890",
                ClaimaintEmail = "johndoe@example.com",
                SettlementForm = "Form123"
            };

            var created = await _repo.Create(report);

            Assert.AreEqual(report.Id, created.Id);
            Assert.AreEqual("John Doe", created.ClaimaintName);
        }

        [Test]
        public void AddException()
        {
            var report = new Report
            {
                Id = 1,
                ClaimaintName = null 
            };

            Assert.ThrowsAsync<CannotCreateException>(async () => await _repo.Create(report));
        }

        [Test]
        public async Task GetAll()
        {
            var report1 = new Report { Id = 1, ClaimaintName = "John Doe" };
            var report2 = new Report { Id = 2, ClaimaintName = "Jane Doe" };

            await _repo.Create(report1);
            await _repo.Create(report2);

            var reports = await _repo.GetAll();

            Assert.AreEqual(2, reports.Count());
        }

        [Test]
        public async Task Get()
        {
            var report = new Report { Id = 1, ClaimaintName = "John Doe" };
            await _repo.Create(report);

            var fetched = await _repo.GetById(report.Id);

            Assert.AreEqual(report.Id, fetched.Id);
            Assert.AreEqual("John Doe", fetched.ClaimaintName);
        }

        [Test]
        public void GetException()
        {
            Assert.ThrowsAsync<CannotFindException>(async () => await _repo.GetById(99)); 
        }

        [Test]
        public async Task Update()
        {
            var report = new Report
            {
                Id = 1,
                ClaimaintName = "John Doe",
                ClaimaintPhone = "1234567890",
                ClaimaintEmail = "johndoe@example.com"
            };

            await _repo.Create(report);

            report.ClaimaintName = "John Smith"; 
            var updated = await _repo.Update(report);

            Assert.AreEqual("John Smith", updated.ClaimaintName);
        }

        [Test]
        public void UpdateException()
        {
            var report = new Report { Id = 1, ClaimaintName = "John Doe" };

            Assert.ThrowsAsync<CannotUpdateException>(async () => await _repo.Update(report));
        }

        [Test]
        public async Task Delete()
        {
            var report = new Report { Id = 1, ClaimaintName = "John Doe" };
            await _repo.Create(report);

            await _repo.Delete(report);

            var reports = await _repo.GetAll();
            Assert.AreEqual(0, reports.Count());
        }

        [Test]
        public void DeleteException()
        {
            var report = new Report { Id = 99 }; 

            Assert.ThrowsAsync<Exception>(async () => await _repo.Delete(report));
        }
    }
}
