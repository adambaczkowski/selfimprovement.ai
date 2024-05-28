using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalApi.Migrations
{
    /// <inheritdoc />
    public partial class goalChanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Goals");

            migrationBuilder.AlterColumn<string>(
                name: "UserAdvancement",
                table: "Goals",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserAdvancement",
                table: "Goals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Goals",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
