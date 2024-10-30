//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using ReportClaim.Contexts;
//using ReportClaim.Models;
//using ReportClaim.Repositories;
//using ReportClaim.Exceptions;
//using System.Threading.Tasks;

//namespace Testing
//{
//    public class ClaimRepositoryTest
//    {
//        DbContextOptions options;
//        ReportClaimContext context;
//        ClaimRepository repository;

//        [SetUp]
//        public void Setup()
//        {
//            options = new DbContextOptionsBuilder<ReportClaimContext>()
//                .UseInMemoryDatabase("PolicyTestDB")
//                .Options;
//            context = new ReportClaimContext(options);
//            repository = new ClaimRepository(context);
//        }

//        [Test]
//        public async Task Create()
//        {
//            var claim = new Claim { ClaimType="Death"};
//            var result = await repository.Create(claim);
//            Assert.AreEqual(claim.ClaimId, result.ClaimId);
//        }

//        [Test]
//        public void CreateException()
//        {
//            Policy policy = new Policy { Id = 1, PolicyNumber = null };
//            Assert.ThrowsAsync<CannotCreateException>(async () => await repository.Create(policy));
//        }

//        [Test]
//        public async Task Delete()
//        {
//            Policy policy = new Policy { Id = 1, PolicyNumber = "P12345" };
//            await repository.Create(policy);
//            var result = await repository.Delete(policy);
//            Assert.AreEqual(policy.Id, result.Id);
//        }

//        [Test]
//        public void DeleteException()
//        {
//            Policy policy = new Policy { Id = 99, PolicyNumber = "NonExistent" };
//            Assert.ThrowsAsync<Exception>(async () => await repository.Delete(policy));
//        }

//        [Test]
//        public async Task GetAll()
//        {
//            Policy policy1 = new Policy { Id = 1, PolicyNumber = "P12345" };
//            Policy policy2 = new Policy { Id = 2, PolicyNumber = "P67890" };
//            await repository.Create(policy1);
//            await repository.Create(policy2);
//            var result = await repository.GetAll();
//            Assert.AreEqual(2, result.Count());
//        }

//        [Test]
//        public async Task GetById()
//        {
//            Policy policy = new Policy { Id = 1, PolicyNumber = "P12345" };
//            await repository.Create(policy);
//            var result = await repository.GetById(1);
//            Assert.AreEqual(policy.Id, result.Id);
//        }

//        [Test]
//        public void GetByIdException()
//        {
//            Assert.ThrowsAsync<CannotFindException>(async () => await repository.GetById(99));
//        }

//        [Test]
//        public async Task Update()
//        {
//            Policy policy = new Policy { Id = 1, PolicyNumber = "P12345" };
//            await repository.Create(policy);
//            policy.PolicyNumber = "P98765";
//            var result = await repository.Update(policy);
//            Assert.AreEqual(policy.PolicyNumber, result.PolicyNumber);
//        }

//        [Test]
//        public void UpdateException()
//        {
//            Policy policy = new Policy { Id = 99, PolicyNumber = "P98765" };
//            Assert.ThrowsAsync<CannotUpdateException>(async () => await repository.Update(policy));
//        }
//    }
//}
