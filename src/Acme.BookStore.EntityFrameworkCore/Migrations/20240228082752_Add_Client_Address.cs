using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Client_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress_City",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress_Country",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress_State",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress_Street",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessAddress_ZipCode",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_City",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_Country",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_State",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_Street",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_ZipCode",
                table: "AppClients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessAddress_City",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "BusinessAddress_Country",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "BusinessAddress_State",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "BusinessAddress_Street",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "BusinessAddress_ZipCode",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_City",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_Country",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_State",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_Street",
                table: "AppClients");

            migrationBuilder.DropColumn(
                name: "HomeAddress_ZipCode",
                table: "AppClients");
        }
    }
}
