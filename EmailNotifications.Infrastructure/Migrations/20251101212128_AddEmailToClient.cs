using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailNotifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "EmailBody",
                table: "Templates");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailBody",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
