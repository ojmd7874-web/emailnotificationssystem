using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmailNotifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestClientAndTemplateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "client1@example.com", "Client 1" },
                    { 2, "client2@example.com", "Client 2" },
                    { 3, "client3@example.com", "Client 3" },
                    { 4, "client4@example.com", "Client 4" },
                    { 5, "client5@example.com", "Client 5" }
                });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "EmailContent", "EmailSubject", "IsHtmlContent", "Name" },
                values: new object[,]
                {
                    { 1, "<!DOCTYPE html>\r\n                                    <html>\r\n                                    <body>\r\n                                        <h2>Hello</h2>\r\n                                        <p>This is a notification from our system.</p>\r\n                                        <p>Promotional data from Template 1:</p>\r\n                                        <p>{{TemplateContent}}</p>\r\n                                        <p><a href=\"{{Url}}\">View Details</a></p>\r\n                                        <p><a href=\"{{UnsubscribeUrl}}\">Unsubscribe</a></p>\r\n                                    </body>\r\n                                    </html>", "Subject Template 1", true, "Template 1" },
                    { 2, "<!DOCTYPE html>\r\n                                    <html>\r\n                                    <body>\r\n                                        <h2>Hello</h2>\r\n                                        <p>This is a notification from our system.</p>\r\n                                        <p>We would like to promote something exciting:</p>\r\n                                        <p>{{TemplateContent}}</p>\r\n                                        <p><a href=\"{{Url}}\">View Details</a></p>\r\n                                        <p><a href=\"{{UnsubscribeUrl}}\">Unsubscribe</a></p>\r\n                                    </body>\r\n                                    </html>", "Subject Template 2", true, "Template 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
