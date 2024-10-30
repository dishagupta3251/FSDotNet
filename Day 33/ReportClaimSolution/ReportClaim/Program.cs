using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReportClaim.Contexts;
using ReportClaim.Interfaces;
using ReportClaim.Misc;
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

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Check if the Authorization header is present
                var authorizationHeader = context.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    Console.WriteLine("No Authorization header found.");
                }
                else if (!authorizationHeader.StartsWith("Bearer "))
                {
                    Console.WriteLine("Authorization header is not a Bearer token.");
                }
                else
                {
                    // Token is present and valid
                    Console.WriteLine("Authorization header found: " + authorizationHeader);
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Authentication failed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated.");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                context.Response.Headers.Add("WWW-Authenticate", "Bearer");
                context.Response.StatusCode = 401; // Unauthorized
                return Task.CompletedTask;
            }
        };
    });
            #endregion
            #region Repositories
            builder.Services.AddScoped<IRepository<Policy, int>, PolicyRepository>();
            builder.Services.AddScoped<IRepository<Claim, int>, ClaimRepository>();
            builder.Services.AddScoped<IRepository<Report, int>, ReportRepository>();
            builder.Services.AddScoped<IRepository<User, string>, UserRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

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
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

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
