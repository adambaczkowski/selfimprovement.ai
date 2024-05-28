using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityApi.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProfiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileImageId",
                table: "UserProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                table: "UserProfiles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "UserProfiles",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserId",
                table: "UserProfiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
