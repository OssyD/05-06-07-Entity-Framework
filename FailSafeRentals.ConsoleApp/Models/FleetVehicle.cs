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
        public string MakeAndModel { get; set; } = null!;

        public int Milage { get; set; }
        public bool NeedService { get; set; }

        public int RentelStationId { get; set; }

        public RentelStation RentelStation { get; set; } = null!;
    }
}
