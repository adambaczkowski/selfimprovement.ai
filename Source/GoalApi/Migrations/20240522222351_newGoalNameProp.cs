using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalApi.Migrations
{
    /// <inheritdoc />
    public partial class newGoalNameProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalFriendlyName",
                table: "Goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalFriendlyName",
                table: "Goals");
        }
    }
}
