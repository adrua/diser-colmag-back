using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class Personajes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColMagComp",
                schema: "ColMag",
                table: "VaritaMagica");

            migrationBuilder.DropColumn(
                name: "AnoNcimiento",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "ColMagCaseaNombre",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.RenameColumn(
                name: "ColMagPersonajeimagen",
                schema: "ColMag",
                table: "Personajes",
                newName: "ColMagPersonajeImagen");

            migrationBuilder.AlterColumn<decimal>(
                name: "ColMagVaritaMagicaLongitud",
                schema: "ColMag",
                table: "VaritaMagica",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "Date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnoNacimiento",
                schema: "ColMag",
                table: "Personajes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColmagCasaId",
                schema: "ColMag",
                table: "Personajes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_ColmagCasaId",
                schema: "ColMag",
                table: "Personajes",
                column: "ColmagCasaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personajes_Casas_ColmagCasaId",
                schema: "ColMag",
                table: "Personajes",
                column: "ColmagCasaId",
                principalSchema: "COLMAG",
                principalTable: "Casas",
                principalColumn: "ColmagCasaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personajes_Casas_ColmagCasaId",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.DropIndex(
                name: "IX_Personajes_ColmagCasaId",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "AnoNacimiento",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "ColmagCasaId",
                schema: "ColMag",
                table: "Personajes");

            migrationBuilder.RenameColumn(
                name: "ColMagPersonajeImagen",
                schema: "ColMag",
                table: "Personajes",
                newName: "ColMagPersonajeimagen");

            migrationBuilder.AlterColumn<int>(
                name: "ColMagVaritaMagicaLongitud",
                schema: "ColMag",
                table: "VaritaMagica",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "ColMagComp",
                schema: "ColMag",
                table: "VaritaMagica",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColMagPersonajeFechaNacimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnoNcimiento",
                schema: "ColMag",
                table: "Personajes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ColMagCaseaNombre",
                schema: "ColMag",
                table: "Personajes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }
    }
}
