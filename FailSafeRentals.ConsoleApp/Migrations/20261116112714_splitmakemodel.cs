using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FailSafeRentals.ConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261116112714_splitmakemodel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Make",
            table: "FleetVehicles",
            type: "nvarchar(40)",
            maxLength: 40,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Model",
            table: "FleetVehicles",
            type: "nvarchar(40)",
            maxLength: 40,
            nullable: false,
            defaultValue: "");

        migrationBuilder.Sql(
            """
        UPDATE FleetVehicles
        SET Make  = LEFT(MakeAndModel, CHARINDEX(' ', MakeAndModel + ' ') - 1),
            Model = LTRIM(SUBSTRING(MakeAndModel, CHARINDEX(' ', MakeAndModel + ' ') + 1, 40))
        """);

        migrationBuilder.DropColumn(
            name: "MakeAndModel",
            table: "FleetVehicles");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
       name: "MakeAndModel",
       table: "FleetVehicles",
       type: "nvarchar(50)",
       maxLength: 50,
       nullable: false,
       defaultValue: "");

        migrationBuilder.Sql(
            "UPDATE FleetVehicles SET MakeAndModel = LTRIM(RTRIM(Make + ' ' + Model))");

        migrationBuilder.DropColumn(name: "Make", table: "FleetVehicles");
        migrationBuilder.DropColumn(name: "Model", table: "FleetVehicles");
    }
}
