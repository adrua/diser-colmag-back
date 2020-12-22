using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class VaritaMagica2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
