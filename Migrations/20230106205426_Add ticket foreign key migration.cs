using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace seminar_API.Migrations
{
    public partial class Addticketforeignkeymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Tickets",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tickets",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "LocationId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedDate",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Tickets",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Locations",
                newName: "Id");
        }
    }
}
