using Microsoft.EntityFrameworkCore;
using ReportClaim.Models;

namespace ReportClaim.Contexts
{
    public class ReportClaimContext:DbContext
    {
        public ReportClaimContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Report to Policy
            modelBuilder.Entity<Report>()
                .HasOne(p => p.Policy)
                .WithMany(p => p.Reports)
                .HasForeignKey(p => p.PolicyId)
                .HasConstraintName("FK_PolicyNumber");

            //Report to ClaimType
            modelBuilder.Entity<Report>()
                .HasOne(c => c.Claim)
                .WithMany(c => c.Reports)
                .HasForeignKey(c => c.ClaimId)
                .HasConstraintName("FK_ClaimType");


        }
    }
}
