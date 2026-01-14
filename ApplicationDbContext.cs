using DoctorAppoinmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppoinmentSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // Configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Appointment → DoctorProfile relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.Doctor)
                .OnDelete(DeleteBehavior.Restrict);

            // Example: DoctorProfile → Specialty relationship
            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.Specialty)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
