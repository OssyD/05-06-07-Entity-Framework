using System.ComponentModel.DataAnnotations;

namespace FailSafeRentals.ConsoleApp.Models;

public class MaintenanceTag
{
    public int Id { get; set; }

    [Required, MaxLength(40)]
    public string TagName { get; set; } = null!;

    public List<FleetVehicle> FleetVehicles { get; set; } = [];
}
