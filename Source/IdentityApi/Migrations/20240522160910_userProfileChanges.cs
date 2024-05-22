using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityApi.Migrations
{
    /// <inheritdoc />
    public partial class userProfileChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageId",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "UserProfiles",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "UserProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "UserProfiles",
                type: "bytea",
                nullable: true);
        }
    }
}
