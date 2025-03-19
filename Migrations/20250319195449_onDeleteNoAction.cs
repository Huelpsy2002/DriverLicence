using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriverLicence.Migrations
{
    /// <inheritdoc />
    public partial class onDeleteNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_NationalNumber",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_NationalNumber",
                table: "Users",
                column: "NationalNumber",
                principalTable: "Persons",
                principalColumn: "NationalNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_NationalNumber",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_NationalNumber",
                table: "Users",
                column: "NationalNumber",
                principalTable: "Persons",
                principalColumn: "NationalNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
