using System;
using System.Collections.Generic;
using System.Text;

namespace FailSafeRentals.ConsoleApp.Models
{
    public class RentelStation
    {
        public int Id { get; set; }
        public string StationCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;

        public List<FleetVehicle> FleetVehicles { get; set; } = [];
    }
}
