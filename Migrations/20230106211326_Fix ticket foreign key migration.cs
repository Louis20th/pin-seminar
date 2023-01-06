using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace seminar_API.Migrations
{
    public partial class Fixticketforeignkeymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_LocationId",
                table: "Tickets",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Locations_LocationId",
                table: "Tickets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Locations_LocationId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_LocationId",
                table: "Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
