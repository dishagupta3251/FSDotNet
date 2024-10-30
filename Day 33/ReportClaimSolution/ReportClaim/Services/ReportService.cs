using AutoMapper;
using ReportClaim.Interfaces;
using ReportClaim.Models.DTO;
using ReportClaim.Models;

public class ReportService : IReportService
{
    private readonly IRepository<Report, int> _repository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment; // To get the web root path for file saving

    public ReportService(IRepository<Report, int> repository, IMapper mapper, IWebHostEnvironment environment)
    {
        _repository = repository;
        _mapper = mapper;
        _environment = environment;
    }

    public async Task<Report> CreateReport(ReportDTO entity)
    {
        try
        {
            var report = new Report
            {
                PolicyId = entity.PolicyId,
                ClaimId = entity.ClaimId,
                ClaimaintName = entity.ClaimaintName,
                ClaimaintPhone = entity.ClaimaintPhone,
                ClaimaintEmail = entity.ClaimaintEmail,
                IncidentDate = entity.IncidentDate
            };

            // Handle file uploads and conversions
            report.PhotoId = await SaveFileAndGetBase64(entity.PhotoId, "PhotoId");
            report.SettlementForm = await SaveFileAndGetBase64(entity.SettlementForm, "SettlementForm");
            report.DeathCertificate = await SaveFileAndGetBase64(entity.DeathCertificate, "DeathCertificate");
            report.PolicyCertificate = await SaveFileAndGetBase64(entity.PolicyCertificate, "PolicyCertificate");
            report.AddressProof = await SaveFileAndGetBase64(entity.AddressProof, "AddressProof");
            report.CancelledCheck = await SaveFileAndGetBase64(entity.CancelledCheck, "CancelledCheck");
            report.Others = await SaveFileAndGetBase64(entity.Others, "Others");

            var newReport = await _repository.Create(report);
            return newReport;
        }
        catch
        {
            throw new Exception("Cannot create report");
        }
    }

    private async Task<string> SaveFileAndGetBase64(IFormFile file, string fileNamePrefix)
    {
        if (file == null || file.Length == 0)
            return string.Empty;

        
        var uniqueFileName = $"{fileNamePrefix}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";

        var uploadsFolderPath = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }
        var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

        // Save the file to the folder
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }

    public async Task<IEnumerable<Report>> GetAllReports()
    {
        try
        {
            var reports = await _repository.GetAll();
            if (reports.Count() <= 0)
                throw new Exception("Cannot provide all policies");
            return reports;
        }
        catch
        {
            throw new Exception("Cannot get all reports");
        }
    }
}
