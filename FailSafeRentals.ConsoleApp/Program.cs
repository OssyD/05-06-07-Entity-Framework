using FailSafeRentals.ConsoleApp.Data;
using FailSafeRentals.ConsoleApp.Models;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;

using var _db = new FailSafeRentalsDbContext();

if (!_db.Vehicles.Any())
{
    var gps = _db.ExtraFeatures.FirstOrDefault(f => f.Name == "GPS Navigation")
              ?? new ExtraFeature { Name = "GPS Navigation", DailyPrice = 75m };

    var car = new Vehicle
    {
        RegistrationNumber = "ABC123",
        Brand = "Volvo",
        Model = "XC60",
        DailyRate = 895m,
        ExtraFeatures = new() { gps }
    };

    var customer = new Customer
    {
        FullName = "Anna Lindqvist",
        Email = "anna@failsafe.se",
        DriverLicenseNumber = "SE-998877"
    };

    _db.Rentals.Add(new Rental
    {
        Customer = customer,
        Vehicle = car,
        StartDate = DateTime.UtcNow,
        TotalCost = 2685m
    });

    _db.SaveChanges();
}

//var rental = _db.Rentals
//    .Include(r => r.Customer)
//    .Include(r => r.Vehicle)
//    .First();

//Console.WriteLine($"Verifierat: {rental.Customer.FullName} hyr {rental.Vehicle.Brand} ({rental.Vehicle.RegistrationNumber})");

//SkapaCustomer();
//SkapaFleraFordon();
//HämtaAllaTillgängligaFordon();  // SELECT * FROM Vehicles WHERE IsAvailable = 1
//HämtaEttSpecifiktFordon();
//UppdateraVehicle();
//TaBortVehicle();

// Innan vi hämtar fram uthyrningar måste vi skapa några fler:
//LäggTillUthyrningar();
//HämtaUthyrningar();
//VisaNPlus1();
//HämtaDelarAvUthyrningar();
//LäsInFrånFil();
//LäggTillTakbox();
//await RekommenderaEfterVädret();
//SäkertVsLivsfarligt();
ExecuteStoredProcedure(1);


if (Debugger.IsAttached)
{
    Console.Write("\n\nTryck på en tangent för att stänga fönstret...");
    Console.ReadKey();
}

void SkapaCustomer()
{
    using var _db = new FailSafeRentalsDbContext();

    var newCustomer = new Customer
    {
        FullName = "Astrid Lindholm",
        Email = "astrid.lindholm@failsafe.se",
        DriverLicenseNumber = "SE-123456789"
    };

    _db.Customers.Add(newCustomer);
    _db.SaveChanges();

    Console.WriteLine($"\n[Info] Kund sparad med Id: {newCustomer.Id}\n");
}

void SkapaFleraFordon()
{
    using var _db = new FailSafeRentalsDbContext();

    var fleetBatch = new List<Vehicle>
    {
        new() { RegistrationNumber = "FS-001", Brand = "Volvo", Model = "V90", DailyRate = 850m, IsAvailable = true},
        new() { RegistrationNumber = "FS-002", Brand = "Polestar", Model = "2", DailyRate = 920m, IsAvailable = true },
        new() { RegistrationNumber = "FS-003", Brand = "Tesla", Model = "Model 3", DailyRate = 980m, IsAvailable = false },
        new() { RegistrationNumber = "FS-004", Brand = "Volkswagen", Model = "ID.4", DailyRate = 760m, IsAvailable = true },
        new() { RegistrationNumber = "FS-005", Brand = "Kia", Model = "EV6", DailyRate = 790m, IsAvailable = true }

    };

    _db.Vehicles.AddRange(fleetBatch);
    _db.SaveChanges();
}

void HämtaAllaTillgängligaFordon()
{
    using var _db = new FailSafeRentalsDbContext();

    List<Vehicle> availableVehicles = _db.Vehicles
            .Where(v => v.IsAvailable).ToList();
    
    Console.WriteLine();
    
    foreach (var vehicle in availableVehicles)
    {
        Console.WriteLine($"{vehicle.Brand} {vehicle.Model} {vehicle.DailyRate} {vehicle.IsAvailable}");
    }
}

void HämtaEttSpecifiktFordon()
{
    using var _db = new FailSafeRentalsDbContext();

    Vehicle? vehicle = _db.Vehicles.Find(1);
    if (vehicle is not null)
    {
        Console.WriteLine($"\nHittade: {vehicle.RegistrationNumber} {vehicle.Brand} {vehicle.Model} ");
    }
}

void UppdateraVehicle()
{
    using var _db = new FailSafeRentalsDbContext();

    // UPDATE: Hämta -> Ändra egenskap -> Spara
    var vehicleToUpdate = _db.Vehicles.Find(1);
    if (vehicleToUpdate is not null)
    {
        vehicleToUpdate.DailyRate = 750m;
        _db.SaveChanges();
        Console.WriteLine($"\nUppdaterade: {vehicleToUpdate.RegistrationNumber} {vehicleToUpdate.Brand} {vehicleToUpdate.Model} ");
    }
}
void TaBortVehicle()
{
    using var _db = new FailSafeRentalsDbContext();

    // DELETE: Hämta -> Markera för borttagning -> Spara
    var vehicleToDelete = _db.Vehicles.Find(99);
    if (vehicleToDelete is not null)
    {
        _db.Vehicles.Remove(vehicleToDelete); 
        _db.SaveChanges();
        Console.WriteLine("\nTog bort fordon 99");
    }
    else
    {
        Console.WriteLine("\nFordon 99 hittades inte.");
    }

}

void LäggTillUthyrningar()
{
    using var _db = new FailSafeRentalsDbContext();

    // Vi har bara 2 kunder, så ge dem fem uthyrningar var
    var uthyrningar = new List<Rental>
    {
        new() { CustomerId = 1, VehicleId = 2, StartDate = DateTime.UtcNow, TotalCost = 1685m},
        new() { CustomerId = 1, VehicleId = 3, StartDate = DateTime.UtcNow, TotalCost = 2685m},
        new() { CustomerId = 1, VehicleId = 4, StartDate = DateTime.UtcNow, TotalCost = 3685m},
        new() { CustomerId = 1, VehicleId = 5, StartDate = DateTime.UtcNow, TotalCost = 4685m},
        new() { CustomerId = 1, VehicleId = 6, StartDate = DateTime.UtcNow, TotalCost = 5685m},

        new() { CustomerId = 2, VehicleId = 2, StartDate = DateTime.UtcNow, TotalCost = 1501m},
        new() { CustomerId = 2, VehicleId = 6, StartDate = DateTime.UtcNow, TotalCost = 2501m},
        new() { CustomerId = 2, VehicleId = 3, StartDate = DateTime.UtcNow, TotalCost = 3501m},
        new() { CustomerId = 2, VehicleId = 4, StartDate = DateTime.UtcNow, TotalCost = 4501m},
        new() { CustomerId = 2, VehicleId = 5, StartDate = DateTime.UtcNow, TotalCost = 5501m},
    };
    _db.Rentals.AddRange(uthyrningar);
    _db.SaveChanges();
}

void HämtaUthyrningar()
{
    using var _db = new FailSafeRentalsDbContext();

    var recentHighValuerentals = _db.Rentals
        .Where(r => r.TotalCost >= 3000m)
        .OrderByDescending(r => r.StartDate)
        .Take(5)
        .ToList();

    Console.WriteLine();
    foreach (var r in recentHighValuerentals)
    {
        Console.WriteLine($"{r.StartDate} {r.CustomerId} {r.TotalCost}");
    }

}

void VisaNPlus1()
{
    using var _db = new FailSafeRentalsDbContext();

    // 1. FÄLLAN (N+1): 1 fråga för Rentals + 1 extra SQL-fråga per varv i loopen!
    var rentals = _db.Rentals.ToList();
    foreach (var r in rentals)
    {
        _db.Entry(r).Reference(x => x.Vehicle).Load(); // Triggar ny SELECT i varje varv!
        Console.WriteLine($"Bokning #{r.Id}: {r.Vehicle.Model}");
    }

    // 2. LÖSNINGEN: Eager Loading (.Include) – EN enda SQL-fråga med INNER JOIN!
    var fastRentals = _db.Rentals.Include(r => r.Vehicle).ToList();
    foreach (var r in fastRentals)
        Console.WriteLine($"Bokning #{r.Id}: {r.Vehicle.Model}");
}

void HämtaDelarAvUthyrningar()
{
    using var _db = new FailSafeRentalsDbContext();

    // Hämta BARA de 4 kolumner vi faktiskt behöver (ingen Change Tracking!)
    var rentalSummary = _db.Rentals
        .Where(r => r.TotalCost > 3000m)
        .Select(r => new { BookingId = r.Id, CustomerName = r.Customer.FullName, CarModel = r.Vehicle.Model, r.TotalCost })
        .ToList();

    Console.WriteLine();

    foreach (var r in rentalSummary)
    {
        Console.WriteLine($"{r.BookingId} {r.CustomerName} {r.CarModel} {r.TotalCost}");
    }
}

void LäsInFrånFil()
{
    using var _db = new FailSafeRentalsDbContext();

    // Läs från filen vehicles.txt
    var valid = new List<Vehicle>();

    var existing = _db.Vehicles.Select(v => v.RegistrationNumber).ToHashSet();

    foreach (var line in File.ReadAllLines("vehicles.txt").Skip(1))
    {
        var c = line.Split('\t');
        if (c.Length < 5) continue;
        string reg = c[0].Trim().ToUpper();
        if (reg.Length is 0 or > 10 || !existing.Add(reg)) continue;
        if (decimal.TryParse(c[3], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate) 
            && bool.TryParse(c[4], out bool avail))
            valid.Add(new Vehicle 
            { 
                RegistrationNumber = reg, 
                Brand = c[1].Trim(), 
                Model = c[2].Trim(), 
                DailyRate = rate, 
                IsAvailable = avail });
    }

    // Lägg in bilarna i databasen
    if (valid.Count > 0)
    {
        _db.Vehicles.AddRange(valid); // Lägger till alla godkända fordon i ett svep
        int savedRows = _db.SaveChanges(); // Kör en samlad batch-INSERT i transaktion!

        Console.WriteLine($"\n[Import] {savedRows} nya fordon sparades i FailSafeRentals!");
    }
}

void LäggTillTakbox()
{
    using var _db = new FailSafeRentalsDbContext();

    var volvo = _db.Vehicles.Include(v => v.ExtraFeatures)
        .FirstOrDefault(v => v.RegistrationNumber == "ABC123");
    var roofBox = new ExtraFeature { Name = "Takbox Thule", DailyPrice = 120m };

    if (volvo is not null)
    {
        volvo.ExtraFeatures.Add(roofBox);
        _db.SaveChanges();
    }

}

async Task RekommenderaEfterVädret()
{
    using var http = new HttpClient();
    string url = "https://api.open-meteo.com/v1/forecast" +
            "?latitude=57.7089&longitude=11.9746&current_weather=true";

    Console.WriteLine("Hämtar aktuellt väder för Göteborg från Open-Meteo...");
    var weather = await http.GetFromJsonAsync<WeatherApiDto>(url);
    double temp = weather?.CurrentWeather.Temperature ?? 0;

    Console.WriteLine($"Aktuell temperatur i Göteborg: {temp:N1} °C");
    Console.WriteLine(temp < 2.0
        ? "Kallt väder (< 2 °C) – rekommenderar trygga Volvobilar:"
        : "Mildare väder (>= 2 °C) – rekommenderar prisvärda bilar under 700 kr/dag:");

    using var _db = new FailSafeRentalsDbContext();
    var recommended = _db.Vehicles
        .Where(v => v.IsAvailable && (temp < 2.0 ? v.Brand == "Volvo" : v.DailyRate < 700m))
        .Take(3)
        .ToList();

    if (recommended.Count == 0)
    {
        Console.WriteLine("Inga lediga bilar matchade dagens väderrekommendation.");
        return;
    }

    foreach (var car in recommended)
    {
        Console.WriteLine($"- {car.Brand} {car.Model} ({car.RegistrationNumber}) – {car.DailyRate:N2} kr/dag");
    }
}

void SäkertVsLivsfarligt()
{
    using var _db = new FailSafeRentalsDbContext();
    string brandInput = "Volvo";

    // LIVSFARLIGT – GÖR ALDRIG SÅ HÄR! (SQL Injection)
    var bad = _db.Vehicles.FromSqlRaw($"SELECT * FROM Vehicles WHERE Brand = '{brandInput}'").ToList();

    // SÄKERT 1: FromSqlInterpolated (skapar automatiskt @p0!)
    var safe1 = _db.Vehicles.FromSqlInterpolated($"SELECT * FROM Vehicles WHERE Brand = {brandInput}").ToList();

    // SÄKERT 2: FromSqlRaw med platshållare {0}
    var safe2 = _db.Vehicles.FromSqlRaw("SELECT * FROM Vehicles WHERE Brand = {0}", brandInput).ToList();

}

void ExecuteStoredProcedure(int id)
{
    using var _db = new FailSafeRentalsDbContext();

    if (id <= 0)
    {
        Console.WriteLine("Ogiltigt ID. Ange ett positivt heltal.");
        return;
    }

    var cars = _db.FleetVehicles
    .FromSql($"EXEC dbo.usp_GetFleetVehiclesFromStation @StationId = {id}")
    .ToList();

    foreach (var car in cars)
    {
        Console.WriteLine($"- {car.Id} {car.Make} ({car.RegistrationNumber})");
    }

}

public record WeatherApiDto(
    [property: JsonPropertyName("current_weather")] CurrentWeatherDto CurrentWeather
);

public record CurrentWeatherDto(
    [property: JsonPropertyName("temperature")] double Temperature
);



