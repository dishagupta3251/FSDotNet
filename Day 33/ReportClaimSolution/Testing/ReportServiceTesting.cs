using Microsoft.EntityFrameworkCore;
using Moq;
using ReportClaim.Contexts;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using ReportClaim.Repositories;

using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace Testing
{
    internal class ReportServiceTest
    {
        DbContextOptions options;
        ReportClaimContext context;
        ReportRepository repository;
        ReportService reportService;
        Mock<IMapper> mapper;
        Mock<IWebHostEnvironment> environment;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ReportClaimContext>()
                .UseInMemoryDatabase("ReportServiceDB")
                .Options;
            context = new ReportClaimContext(options);
            repository = new ReportRepository(context);
            mapper = new Mock<IMapper>();
            environment = new Mock<IWebHostEnvironment>();
            reportService = new ReportService(repository, mapper.Object, environment.Object);
        }

        [Test]
        public async Task CreateReport()
        {
            var reportDTO = new ReportDTO
            {
                PolicyId = 1,
                ClaimId = 1,
                ClaimaintName = "John Doe",
                ClaimaintPhone = "1234567890",
                ClaimaintEmail = "johndoe@example.com",
                IncidentDate = DateTime.Now,
                PhotoId = null,
                SettlementForm = null,
                DeathCertificate = null,
                PolicyCertificate = null,
                AddressProof = null,
                CancelledCheck = null,
                Others = null
            };

            var report = new Report
            {
                Id = 1,
                PolicyId = reportDTO.PolicyId,
                ClaimId = reportDTO.ClaimId,
                ClaimaintName = reportDTO.ClaimaintName,
                ClaimaintPhone = reportDTO.ClaimaintPhone,
                ClaimaintEmail = reportDTO.ClaimaintEmail,
                IncidentDate = reportDTO.IncidentDate
            };

            mapper.Setup(m => m.Map<Report>(reportDTO)).Returns(report);
            

            var result = await reportService.CreateReport(reportDTO);
            Assert.AreEqual(report, result);
        }

        [Test]
        public async Task CreateReportException()
        {
            var reportDTO = new ReportDTO { PolicyId = 1, ClaimId = 1, ClaimaintName = null };
            Assert.ThrowsAsync<Exception>(async () => await reportService.CreateReport(reportDTO));
        }

        [Test]
        public async Task GetAllReports()
        {
            var reportDTO = new ReportDTO
            {
                PolicyId = 1,
                ClaimId = 1,
                ClaimaintName = "John Doe",
                ClaimaintPhone = "1234567890",
                ClaimaintEmail = "johndoe@example.com",
                IncidentDate = DateTime.Now
            };

            var report = new Report
            {
                Id = 1,
                PolicyId = reportDTO.PolicyId,
                ClaimId = reportDTO.ClaimId,
                ClaimaintName = reportDTO.ClaimaintName,
                ClaimaintPhone = reportDTO.ClaimaintPhone,
                ClaimaintEmail = reportDTO.ClaimaintEmail,
                IncidentDate = reportDTO.IncidentDate
            };

            await reportService.CreateReport(reportDTO);
            var result = await reportService.GetAllReports();
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAllReportsException()
        {
            
            Assert.ThrowsAsync<Exception>(async () => await reportService.GetAllReports());
        }
    }
}
