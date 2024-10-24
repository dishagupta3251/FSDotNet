using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReportClaim.Contexts;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using ReportClaim.Repositories;
using ReportClaim.Services;

namespace Testing
{
    internal class ClaimServiceTest
    {
        DbContextOptions options;
        ReportClaimContext context;
        ClaimRepository repository;
        ClaimService claimService;
        Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ReportClaimContext>()
                .UseInMemoryDatabase("ClaimServiceDB")
                .Options;
            context = new ReportClaimContext(options);
            repository = new ClaimRepository(context);
            mapper = new Mock<IMapper>();
            claimService=new ClaimService(repository,mapper.Object);
        }

        [Test]
        public async Task CreateClaimService()
        {
            var claimDTO = new ClaimDTO
            {
                ClaimType = "Accident"
            };
            var claim = new Claim
            {
                ClaimId = 1,
                ClaimType = claimDTO.ClaimType
            };
            mapper.Setup(m=>m.Map<Claim>(claimDTO)).Returns(claim);
            var result=await claimService.CreateClaim(claimDTO);
            Assert.AreEqual(claim, result);

        }
        [Test]
        public async Task CreateException()
        {
            ClaimDTO claim = new ClaimDTO {  ClaimType = null };
            mapper.Setup(m => m.Map<Claim>(claim)).Throws(new Exception("Cannot add claim"));
            Assert.ThrowsAsync<Exception>(async () => await claimService.CreateClaim(claim));
        }
        [Test]
        public async Task GetAllTest()
        {
            var claimDTO = new ClaimDTO
            {
                ClaimType = "Accident"
            };
            var claim = new Claim
            {
                ClaimId = 1,
                ClaimType = claimDTO.ClaimType
            };
            mapper.Setup(m => m.Map<Claim>(claimDTO)).Returns(claim);
            await claimService.CreateClaim(claimDTO);
            var result=claimService.GetAllClaims();
            Assert.NotNull(result);
        }
        [Test]
        public async Task GetAllException()
        {
            Assert.ThrowsAsync<Exception>(async () => await claimService.GetAllClaims());
        }
    }
}
