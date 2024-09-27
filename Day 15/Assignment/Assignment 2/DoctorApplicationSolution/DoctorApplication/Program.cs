using DoctorApplication.Interfaces;
using DoctorApplication.Models;
using DoctorApplication.Respositories;
using DoctorApplication.Services;

namespace DoctorApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Repository Injection
            builder.Services.AddScoped<IRepository<int, Doctor>, DoctorRepository>();
            builder.Services.AddScoped<IRepository<int, Appointment>, AppointmentRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            #endregion

            #region Service Injection
            builder.Services.AddScoped<IBookAppointmentServices, BookAppointmentServices>();
            builder.Services.AddScoped<ILoginUserService, LoginUserServices>();
            #endregion

         

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LoginController}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
