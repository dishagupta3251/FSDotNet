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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seats> Seats { get; set; }
        public DbSet<AvailableRoute> AvailableRoutes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<BusOperator> BusOperators { get; set; }
        public DbSet<SeatsBooked> SeatsBooked {  get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               
            modelBuilder.Entity<Bus>()
                .HasMany(b => b.Schedules)
                .WithOne(s => s.Bus)
                .HasForeignKey(s => s.BusId)
                .HasConstraintName("FK_Schedule_Bus")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                .HasOne(b=>b.Operator)
                .WithMany(o=>o.Buses)
                .HasForeignKey(b=>b.OperatorID)
                .HasConstraintName("FK_Bus_BusOperator")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bus>()
                .HasOne(b => b.AvailableRoutes)
                .WithMany(r => r.Buses)
                .HasForeignKey(b => b.RouteId)
                .HasConstraintName("FK_Bus_RouteId");

            modelBuilder.Entity<Seats>()
                .HasOne(s => s.Bus)
                .WithMany(b => b.Seats)
                .HasForeignKey(s => s.BusId)
                .HasConstraintName("FK_Buse_Seat")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Operator)
                .WithMany(o => o.Reviews)
                .HasForeignKey(r => r.OperatorId)
                .HasConstraintName("FK_Review_Operator");

            modelBuilder.Entity<Customer>()
                  .HasOne(c => c.User)
                  .WithOne()
                  .HasForeignKey<Customer>(c => c.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

         modelBuilder.Entity<SeatsBooked>()
                .HasOne(sb=>sb.Seats)
                .WithOne(s=>s.SeatsBooked)
                .HasForeignKey<SeatsBooked>(s => s.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatsBooked>()
              .HasOne(sb => sb.Bus)
              .WithMany(b => b.SeatsBooked)
              .HasForeignKey(s => s.BusId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatsBooked>()
              .HasOne(sb => sb.Customer)
              .WithMany(c => c.Seats)
              .HasForeignKey(s => s.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeatsBooked>()
              .HasOne(sb => sb.Booking)
              .WithMany(b => b.SeatsBooked)
              .HasForeignKey(s => s.BookingId)
              .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<BusSchedule>()
                .HasOne(sb => sb.AvailableRoute)
                .WithMany(r => r.Schedules)
                .HasForeignKey(sb => sb.RouteId)
                .HasConstraintName("FK_BusSchedule_AvailableRoute");

            modelBuilder.Entity<Bus>()
                .Property(b => b.BusType)
                .HasConversion<string>();

            modelBuilder.Entity<BusSchedule>()
                .Property(bs=>bs.Day)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u=>u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p=>p.Type)
                .HasConversion<string>();

                

            modelBuilder.Entity<Payment>()
        .HasOne(p => p.Booking)
        .WithOne(b => b.Payment)
        .HasForeignKey<Payment>(p => p.BookingId)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("FK_Payment_Booking");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .HasConstraintName("FK_Booking_Customer");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Bus)
                .WithMany(b => b.Booking)
                .HasForeignKey(b => b.BusId)
                .HasConstraintName("FK_Booking_Bus");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Route)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RouteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Booking_Routes");

     

            



                
        }
    }
}
