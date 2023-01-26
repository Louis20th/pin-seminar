using Microsoft.EntityFrameworkCore.Migrations;

namespace seminar_API.Migrations
{
    public partial class addcoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Locations");

            migrationBuilder.AddColumn<decimal>(
                name: "latitude",
                table: "Locations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "longitude",
                table: "Locations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
