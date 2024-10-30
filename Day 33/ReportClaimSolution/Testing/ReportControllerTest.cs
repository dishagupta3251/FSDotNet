//using NUnit.Framework;
//using Microsoft.AspNetCore.Mvc;
//using ReportClaim.Interfaces;
//using ReportClaim.Models.DTO;
//using ReportClaim.Controllers;
//using ReportClaim.Services;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using ReportClaim.Contexts;

//namespace ReportClaim.Tests
//{

//    public class ReportControllerTest
//    {
//        ReportController _controller;
//        ReportClaimContext _context;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<ReportClaimContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            _context = new ReportClaimContext(options);
//            var policyService = new PolicyService(_context);
//            var claimService = new ClaimService(_context);
//            var reportService = new ReportService(_context);
//            _controller = new ReportController(policyService, claimService, reportService);
//        }

//        [Test]
//        public async Task GetPolicies_ReturnsOk_WithPolicyNumbers()
//        {
//            _context.Policies.Add(new PolicyDTO { PolicyNumber = "P123" });
//            _context.Policies.Add(new PolicyDTO { PolicyNumber = "P456" });
//            await _context.SaveChangesAsync();

//            var result = await _controller.GetPolicies();

//            var okResult = (OkObjectResult)result;
//            var policyNumbers = (List<string>)okResult.Value;
//            Assert.AreEqual(2, policyNumbers.Count);
//            Assert.AreEqual("P123", policyNumbers[0]);
//            Assert.AreEqual("P456", policyNumbers[1]);
//        }

//        [Test]
//        public async Task GetPolicies_ReturnsNotFound_WhenNoPolicies()
//        {
//            var result = await _controller.GetPolicies();
//            var notFoundResult = (NotFoundObjectResult)result;
//            Assert.AreEqual("No policies found.", notFoundResult.Value);
//        }

//        [Test]
//        public async Task GetClaims_ReturnsOk_WithClaimTypes()
//        {
//            _context.Claims.Add(new ClaimDTO { ClaimType = "ClaimType1" });
//            _context.Claims.Add(new ClaimDTO { ClaimType = "ClaimType2" });
//            await _context.SaveChangesAsync();

//            var result = await _controller.GetClaims();

//            var okResult = (OkObjectResult)result;
//            var claimTypes = (List<string>)okResult.Value;
//            Assert.AreEqual(2, claimTypes.Count);
//            Assert.AreEqual("ClaimType1", claimTypes[0]);
//            Assert.AreEqual("ClaimType2", claimTypes[1]);
//        }

//        [Test]
//        public async Task GetClaims_ReturnsNotFound_WhenNoClaims()
//        {
//            var result = await _controller.GetClaims();
//            var notFoundResult = (NotFoundObjectResult)result;
//            Assert.AreEqual("No claims found.", notFoundResult.Value);
//        }

//        [Test]
//        public async Task CreateReport_ReturnsOk_WithReportId()
//        {
//            var reportDTO = new ReportDTO { /* Set necessary properties */ };
//            var result = await _controller.CreateReport(reportDTO);
//            var okResult = (OkObjectResult)result;
//            Assert.IsNotNull(okResult.Value);
//        }

//        [Test]
//        public async Task CreateReport_ReturnsBadRequest_WhenReportDTOIsNull()
//        {
//            var result = await _controller.CreateReport(null);
//            var badRequestResult = (BadRequestObjectResult)result;
//            Assert.AreEqual(400, badRequestResult.StatusCode);
//        }

//        [Test]
//        public async Task CreateReport_ReturnsInternalServerError_OnException()
//        {
//            var reportDTO = new ReportDTO { /* Set necessary properties */ };
//            _context.Database.EnsureDeleted();

//            var result = await _controller.CreateReport(reportDTO);
//            var objectResult = (ObjectResult)result;
//            Assert.AreEqual(500, objectResult.StatusCode);
//            Assert.IsNotNull(objectResult.Value);
//        }
//    }
//}
