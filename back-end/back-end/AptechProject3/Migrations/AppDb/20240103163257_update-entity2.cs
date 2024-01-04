using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptechProject3.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class updateentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ClientServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ClientServices");
        }
    }
}
