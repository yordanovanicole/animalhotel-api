using Microsoft.EntityFrameworkCore;
using animalhotelAPI.Models;

namespace animalhotelAPI.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<PetType>()
                .HasMany(pt => pt.Pets)
                .WithOne(p => p.PetType)
                .HasForeignKey(p => p.PetTypeId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Reservations)
                .WithOne(res => res.Room)
                .HasForeignKey(res => res.RoomId);

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Reservations)
                .WithOne(r => r.Pet)
                .HasForeignKey(r => r.PetId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Reviews)
                .WithOne(rv => rv.Reservation)
                .HasForeignKey(rv => rv.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Reservations)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Services)
                .WithMany(s => s.Reservations);

            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Extras)
                .WithMany(e => e.Reservations);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Services)
                .WithMany(s => s.Employees);

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Services)
                .WithMany(s => s.Pets);
        }
    }
}
