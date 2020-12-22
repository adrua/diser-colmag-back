using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class VaritaMagica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Madera",
                schema: "ColMag",
                table: "VaritaMagica");

            migrationBuilder.AddColumn<string>(
                name: "ColMagVaritaMagicaMadera",
                schema: "ColMag",
                table: "VaritaMagica",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColMagVaritaMagicaMadera",
                schema: "ColMag",
                table: "VaritaMagica");

            migrationBuilder.AddColumn<string>(
                name: "Madera",
                schema: "ColMag",
                table: "VaritaMagica",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
