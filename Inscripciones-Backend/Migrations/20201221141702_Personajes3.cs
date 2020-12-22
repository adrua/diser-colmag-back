using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class Personajes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnoNacimiento",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.AlterColumn<decimal>(
                name: "ColMagVaritaMagicaLongitud",
                schema: "ColMag",
                table: "VaritaMagica",
                type: "decimal(5, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ColMagComp",
                schema: "ColMag",
                table: "VaritaMagica",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColMagPersonajeAnoNacimiento",
                schema: "ColMag",
                table: "Personajes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColMagComp",
                schema: "ColMag",
                table: "VaritaMagica");

            migrationBuilder.DropColumn(
                name: "ColMagPersonajeAnoNacimiento",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.AlterColumn<decimal>(
                name: "ColMagVaritaMagicaLongitud",
                schema: "ColMag",
                table: "VaritaMagica",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5, 2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnoNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "int",
                nullable: true);
        }
    }
}
