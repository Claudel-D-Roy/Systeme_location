using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tp1_CoreApplication.Data;
using Tp1_CoreApplication.Domain;

namespace Tp1_CoreApplication
{
    public class Tp1_Context : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public bool IsAlreadySeeded { get; set; } = false;

        public Tp1_Context(DbContextOptions<Tp1_Context> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Branch>()
                .HasMany(car => car.Cars)
                .WithOne(branch => branch.branch)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .HasMany(r => r.Rental)
                .WithOne(c => c.car)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasMany(d => d.drivers)
                .WithOne(r => r.rental)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasMany(d => d.notes)
                .WithOne(r => r.rental)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Car>()
                .HasMany(d => d.notes)
                .WithOne(r => r.car)
                .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}


