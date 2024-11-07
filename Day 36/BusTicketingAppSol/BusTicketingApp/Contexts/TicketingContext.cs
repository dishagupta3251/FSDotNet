using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Contexts
{
    public class TicketingContext:DbContext
    {
        public TicketingContext(DbContextOptions dbContextOption):base(dbContextOption)
        {
            
        }

        public DbSet<User> Users {  get; set; }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<AvailableRoute> AvailableRoutes { get; set; }

        public DbSet<BusOperator> BusOperators { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>();
        //    modelBuilder.Entity<Bus>()
        //        .HasIndex(b => b.BusName)
        //        .IsUnique();
        //}
    }
}
