using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace seminar_API.Migrations
{
    public partial class FKchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Locations_LocationId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Tickets",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_LocationId",
                table: "Tickets",
                newName: "IX_Tickets_LocationID");

            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Tickets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationID",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Locations_LocationID",
                table: "Tickets",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Locations_LocationID",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Tickets",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_LocationID",
                table: "Tickets",
                newName: "IX_Tickets_LocationId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Locations_LocationId",
                table: "Tickets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
