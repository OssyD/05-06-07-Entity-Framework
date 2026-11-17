using System.ComponentModel.DataAnnotations;

namespace FailSafeRentals.ConsoleApp.Models;

public class DamageReport
{
    public int Id { get; set; }

    [Required, MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required, MaxLength(20)]
    public string Severity { get; set; } = null!;

    public decimal EstimatedCost { get; set; }

    public DateTime ReportedAt { get; set; }

    public DateTime? RepairedAt { get; set; }

    public int FleetVehicleId { get; set; }
    public FleetVehicle FleetVehicle { get; set; } = null!;
}
