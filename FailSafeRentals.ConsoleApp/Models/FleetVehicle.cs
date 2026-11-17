using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FailSafeRentals.ConsoleApp.Models
{
    public class FleetVehicle
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string RegistrationNumber { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Make { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Model { get; set; } = null!;

        //[Required, MaxLength(50)]
        //public string MakeAndModel { get; set; } = null!;

        [Required]
        public int MileageKm { get; set; }
        public bool NeedsService { get; set; }

        public int RentalStationId { get; set; }

        public RentalStation RentalStation { get; set; } = null!;

        public List<DamageReport> DamageReports { get; set; } = [];

        public List<MaintenanceTag> MaintenanceTags { get; set; } = [];
    }
}
