
using EF_WebAPI.Interfaces;
using EF_WebAPI.Models;
using EF_WebAPI.Repositories;
using EF_WebAPI.Services;
using EFCoreFirstAPI.Contexts;
using EFCoreFirstAPI.Interfaces;
using EFCoreWebAPI.Repositories;
using EFCoreWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EF_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Contexts
            builder.Services.AddDbContext<ShoppingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repository
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<int, Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<int, ProductImage>, ImageRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            #endregion
            #region Services
            builder.Services.AddScoped<ICustomerBasicService, CustomerBasicService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductImage, ProductImageService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion

            #region Mapper
            builder.Services.AddAutoMapper(typeof(Product));
            builder.Services.AddAutoMapper(typeof(User));
            
            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
