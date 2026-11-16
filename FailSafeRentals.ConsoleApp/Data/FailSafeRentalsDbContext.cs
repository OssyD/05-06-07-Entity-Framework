using Microsoft.EntityFrameworkCore;

using FailSafeRentals.ConsoleApp.Models;

namespace FailSafeRentals.ConsoleApp.Data;

public class FailSafeRentalsDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<Rental> Rentals { get; set; } = null!;
    public DbSet<ExtraFeature> ExtraFeatures { get; set; } = null!;
    public DbSet<RentalStation> RentalStations { get; set; } = null!;
    public DbSet<DamageReport> DamageReports { get; set; } = null!;
    public DbSet<FleetVehicle> FleetVehicles { get; set; } = null!;
    public DbSet<MaintenanceTag> MaintenanceTags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-0Q2CI4E\MSSQLSERVER01;Database=FailSafeRentals;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Unikt index på registreringsnummer
        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.RegistrationNumber)
            .IsUnique();

        // 2. Explicit precision för pengar i LocalDB
        modelBuilder.Entity<Vehicle>()
            .Property(v => v.DailyRate)
            .HasPrecision(10, 2);

        // 3. Shadow Property för SecOps-spårbarhet
        modelBuilder.Entity<Rental>()
            .Property<DateTime>("CreatedAt")
            .HasDefaultValueSql("GETUTCDATE()");

        // 4. Skydda uthyrningshistorik mot oavsiktlig radering
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RentalStation>()
            .HasIndex(rs => rs.StationCode)
            .IsUnique();

        modelBuilder.Entity<FleetVehicle>()
            .HasIndex(fv => fv.RegistrationNumber)
            .IsUnique();

        modelBuilder.Entity<DamageReport>()
            .Property(dr => dr.EstimatedCost)
            .HasPrecision(10, 2);

        modelBuilder.Entity<DamageReport>()
            .Property<DateTime>("AuditLoggedAt")
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<FleetVehicle>()
            .HasOne(fv => fv.RentalStation)
            .WithMany(rs => rs.FleetVehicles)
            .HasForeignKey(fv => fv.RentalStationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DamageReport>()
            .HasOne(dr => dr.FleetVehicle)
            .WithMany(fv => fv.DamageReports)
            .HasForeignKey(dr => dr.FleetVehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RentalStation>()
            .HasData(
                new RentalStation { Id = 1, StationCode = "GOT-01", City = "Gothenburg", Address = "landvettern 1" },
                new RentalStation { Id = 2, StationCode = "STO-02", City = "Stockholm", Address = "Sveavägen 2" },
                new RentalStation { Id = 3, StationCode = "MAL-03", City = "Malmö", Address = "Third Street 3" }
            );
    }
}
