using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nameaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AdressId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Users",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AdressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Users",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                newName: "IX_Users_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
