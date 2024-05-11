using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalApi.Migrations
{
    /// <inheritdoc />
    public partial class goalChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expirience",
                table: "Goals",
                newName: "Experience");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Goals",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Goals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Goals",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Goals",
                newName: "Expirience");
        }
    }
}
