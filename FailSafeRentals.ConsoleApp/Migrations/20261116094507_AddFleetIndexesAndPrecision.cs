using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FailSafeRentals.ConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261116094507_AddFleetIndexesAndPrecision : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "FullName",
            table: "Customers",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Customers",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "DriverLicenseNumber",
            table: "Customers",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.CreateTable(
            name: "RentalStations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StationCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RentalStations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "FleetVehicles",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RegistrationNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                MakeAndModel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                MileageKm = table.Column<int>(type: "int", nullable: false),
                NeedsService = table.Column<bool>(type: "bit", nullable: false),
                RentalStationId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FleetVehicles", x => x.Id);
                table.ForeignKey(
                    name: "FK_FleetVehicles_RentalStations_RentalStationId",
                    column: x => x.RentalStationId,
                    principalTable: "RentalStations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DamageReports",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                Severity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                EstimatedCost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                ReportedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                RepairedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                FleetVehicleId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DamageReports", x => x.Id);
                table.ForeignKey(
                    name: "FK_DamageReports_FleetVehicles_FleetVehicleId",
                    column: x => x.FleetVehicleId,
                    principalTable: "FleetVehicles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_DamageReports_FleetVehicleId",
            table: "DamageReports",
            column: "FleetVehicleId");

        migrationBuilder.CreateIndex(
            name: "IX_FleetVehicles_RegistrationNumber",
            table: "FleetVehicles",
            column: "RegistrationNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_FleetVehicles_RentalStationId",
            table: "FleetVehicles",
            column: "RentalStationId");

        migrationBuilder.CreateIndex(
            name: "IX_RentalStations_StationCode",
            table: "RentalStations",
            column: "StationCode",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DamageReports");

        migrationBuilder.DropTable(
            name: "FleetVehicles");

        migrationBuilder.DropTable(
            name: "RentalStations");

        migrationBuilder.AlterColumn<string>(
            name: "FullName",
            table: "Customers",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Customers",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "DriverLicenseNumber",
            table: "Customers",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50);
    }
}
