using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptechProject3.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class editentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpiredDay",
                table: "ClientServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartDay",
                table: "ClientServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDay",
                table: "ClientServices");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "ClientServices");
        }
    }
}
