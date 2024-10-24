using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ReportClaim.Contexts;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using ReportClaim.Repositories;
using ReportClaim.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testing
{
    internal class PolicyServiceTest
    {
        DbContextOptions options;
        ReportClaimContext context;
        PolicyRepository repository;
        PolicyService policyService;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ReportClaimContext>()
                .UseInMemoryDatabase("PolicyServiceDB")
                .Options;
            context = new ReportClaimContext(options);
            repository = new PolicyRepository(context);
            mapper = new Mock<IMapper>();
            policyService = new PolicyService(repository, mapper.Object);
        }

        [Test]
        public async Task AddPolicy()
        {
            var policyDTO = new PolicyDTO
            {
                PolicyNumber = "PN12345"
             
            };
            var policy = new Policy
            {
                Id = 1,
                PolicyNumber = policyDTO.PolicyNumber
            };
            mapper.Setup(m => m.Map<Policy>(policyDTO)).Returns(policy);
            var result = await policyService.CreatePolicy(policyDTO);
            Assert.AreEqual(policy, result);
        }

        [Test]
        public void AddPolicyFail()
        {
            var policyDTO = new PolicyDTO
            {
                PolicyNumber = null
            };
            mapper.Setup(m => m.Map<Policy>(policyDTO)).Throws(new Exception("Cannot add policy"));
            Assert.ThrowsAsync<Exception>(async () => await policyService.CreatePolicy(policyDTO));
        }

        [Test]
        public async Task GetAllPolicies()
        {
            var policies = new List<Policy>
            {
                new Policy { Id = 1, PolicyNumber = "PN12345" },
                new Policy { Id = 2, PolicyNumber = "PN54321" }
            };

            context.Policies.AddRange(policies);
            await context.SaveChangesAsync();
            var result = await policyService.GetAllPolicies();
            Assert.NotNull(result);
            
        }

        [Test]
        public async Task GetAllPoliciesFail()
        {
            Assert.ThrowsAsync<Exception>(async () => await policyService.GetAllPolicies());
            
        }
    }
}
