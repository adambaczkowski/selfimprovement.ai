using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalApi.Migrations
{
    /// <inheritdoc />
    public partial class addingNewData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeAvailability",
                table: "Goals",
                newName: "TimeAvailabilityPerWeek");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeAvailabilityPerDay",
                table: "Goals",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserAdvancement",
                table: "Goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserInput",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "GoalTasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "TimeAvailabilityPerDay",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "UserAdvancement",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "UserInput",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "GoalTasks");

            migrationBuilder.RenameColumn(
                name: "TimeAvailabilityPerWeek",
                table: "Goals",
                newName: "TimeAvailability");
        }
    }
}
