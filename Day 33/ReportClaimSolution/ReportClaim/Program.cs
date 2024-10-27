using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using ReportClaim.Contexts;
using ReportClaim.Filter;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Repositories;
using ReportClaim.Services;

namespace ReportClaim
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Context
            builder.Services.AddDbContext<ReportClaimContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Filters
            builder.Services.AddScoped<ReportExceptionFilter>();
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<Policy, int>, PolicyRepository>();
            builder.Services.AddScoped<IRepository<Claim, int>, ClaimRepository>();
            builder.Services.AddScoped<IRepository<Report, int>, ReportRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            #endregion

            #region Mapper
            builder.Services.AddAutoMapper(typeof(Policy));
            builder.Services.AddAutoMapper(typeof(Claim));
            builder.Services.AddAutoMapper(typeof(Report));
            #endregion

            // Add services to the container.

            builder.Services.AddControllers();

            // Enable file uploads with specific limits (optional)
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // Limit file size to 10 MB (adjust as needed)
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Serve static files from wwwroot
            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
