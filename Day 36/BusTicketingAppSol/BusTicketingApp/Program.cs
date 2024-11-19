using System.Text;
using BusTicketingApp.Contexts;
using BusTicketingApp.EmailInterface;
using BusTicketingApp.EmailModels;
using BusTicketingApp.EmailService;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Repositories;
using BusTicketingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BusTicketingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Contexts
            builder.Services.AddDbContext<TicketingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
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
                });
            #endregion

            #region Repository
            builder.Services.AddScoped<IRepository<User,string>,UserRepository>();
            builder.Services.AddScoped<IRepository<AvailableRoute,int>,AvailableRouteRepository>();
            builder.Services.AddScoped<IRepository<Bus, int>, BusRepository>();
            builder.Services.AddScoped<IRepository<Seats, int>, SeatRepository>();
            builder.Services.AddScoped<IRepository<BusOperator, int>, BusOperatorRepository>();
            builder.Services.AddScoped<IRepository<Booking, int>, BookingRepository>();
            builder.Services.AddScoped<IRepository<Customer, int>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<BusSchedule, int>, BusScheduleRepository>();
            builder.Services.AddScoped<IRepository<Payment, int>, PaymentRepository>();
            builder.Services.AddScoped<IRepository<SeatsBooked, int>, SeatsBookedRepository>();
            builder.Services.AddScoped<IRepository<Review, int>, ReviewRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IUserServices, UserService>();
            builder.Services.AddScoped<IRoutingService, RoutingService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IBusOperatorService, BusOperatorService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ISeatService, SeatService>(); 
            builder.Services.AddScoped<IEmailSender, EmailSender>(); 
            
            builder.Services.AddScoped<IBusService, BusService>();
            #endregion

            #region OtherServices
            builder.Services.AddAutoMapper(typeof(User));
            builder.Services.AddAutoMapper(typeof (AvailableRoute));
            builder.Services.AddAutoMapper(typeof(Bus));
            builder.Services.AddAutoMapper(typeof(Customer));
            builder.Services.AddAutoMapper(typeof(BusSchedule));
            builder.Services.AddAutoMapper(typeof(BusOperator));
            builder.Services.AddAutoMapper(typeof(Payment));
            builder.Services.AddAutoMapper(typeof(Review));
            #endregion


            var emailConfig = builder.Configuration
           .GetSection("EmailConfiguration")
           .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);












            builder.Services.AddControllers();
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

            builder.Logging.AddConsole();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
