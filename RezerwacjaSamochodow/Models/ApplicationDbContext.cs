using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RezerwacjaSamochodow.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet for Cars
    public DbSet<Car> Cars { get; set; }

    // DbSet for Reservations
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed data for Cars
        builder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                PricePerDay = 50.00m,
                Owner = "Marek"

            },
            new Car
            {
                Id = 2,
                Brand = "Ford",
                Model = "Focus",
                Year = 2019,
                PricePerDay = 45.00m,
                Owner = "Mirek"
            },
            new Car
            {
                Id = 3,
                Brand = "BMW",
                Model = "3 Series",
                Year = 2021,
                PricePerDay = 80.00m,
                Owner = "Anna"
            },
            new Car
            {
                Id = 4,
                Brand = "Tesla",
                Model = "Model 3",
                Year = 2022,
                PricePerDay = 100.00m,
                Owner = "Jan"
            },
            new Car
            {
                Id = 5,
                Brand = "Honda",
                Model = "Civic",
                Year = 2018,
                PricePerDay = 40.00m,
                Owner = "Maja"
            }
        );
        builder.Entity<Reservation>()
            .Property(r => r.StartDate)
            .HasConversion(
                v => v.ToUniversalTime(),  
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));  
        builder.Entity<Reservation>()
            .Property(r => r.EndDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }

}