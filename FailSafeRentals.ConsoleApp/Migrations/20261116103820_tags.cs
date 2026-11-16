using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FailSafeRentals.ConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261116103820_tags : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "MaintenanceTags",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TagName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MaintenanceTags", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "FleetVehicleMaintenanceTag",
            columns: table => new
            {
                FleetVehiclesId = table.Column<int>(type: "int", nullable: false),
                MaintenanceTagsId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FleetVehicleMaintenanceTag", x => new { x.FleetVehiclesId, x.MaintenanceTagsId });
                table.ForeignKey(
                    name: "FK_FleetVehicleMaintenanceTag_FleetVehicles_FleetVehiclesId",
                    column: x => x.FleetVehiclesId,
                    principalTable: "FleetVehicles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FleetVehicleMaintenanceTag_MaintenanceTags_MaintenanceTagsId",
                    column: x => x.MaintenanceTagsId,
                    principalTable: "MaintenanceTags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FleetVehicleMaintenanceTag_MaintenanceTagsId",
            table: "FleetVehicleMaintenanceTag",
            column: "MaintenanceTagsId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FleetVehicleMaintenanceTag");

        migrationBuilder.DropTable(
            name: "MaintenanceTags");
    }
}
