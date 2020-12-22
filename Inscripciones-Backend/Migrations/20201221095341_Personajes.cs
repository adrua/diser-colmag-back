using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class Personajes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ColMag");

            migrationBuilder.CreateTable(
                name: "Personajes",
                schema: "ColMag",
                columns: table => new
                {
                    ColMagPersonajeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColMagPersonajeNombre = table.Column<string>(maxLength: 75, nullable: true),
                    ColMagPersonajeEspecie = table.Column<string>(maxLength: 20, nullable: true),
                    Genero = table.Column<string>(maxLength: 10, nullable: true),
                    ColMagCaseaNombre = table.Column<string>(maxLength: 10, nullable: true),
                    ColMagPersonajeFechaNacimiento = table.Column<string>(maxLength: 20, nullable: true),
                    AnoNcimiento = table.Column<int>(nullable: false),
                    ColMagPersonajeAscendencia = table.Column<string>(maxLength: 29, nullable: true),
                    ColMagPersonajeColorOjos = table.Column<string>(maxLength: 20, nullable: true),
                    ColMagPersonajeColorCabello = table.Column<string>(maxLength: 20, nullable: true),
                    ColMagPersonajePatronus = table.Column<string>(maxLength: 30, nullable: true),
                    ColMagPersonajeEstudiante = table.Column<bool>(type: "bit", nullable: false),
                    ColMagPersonajeProfesor = table.Column<bool>(type: "bit", nullable: false),
                    ColMagPersonajeActor = table.Column<string>(maxLength: 75, nullable: true),
                    ColMagPersonajeVive = table.Column<bool>(type: "bit", nullable: false),
                    ColMagPersonajeimagen = table.Column<string>(maxLength: 250, nullable: true),
                    Fecha_Computador = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Fecha_Impresion = table.Column<DateTime>(nullable: true),
                    Fecha_Reimpresion = table.Column<DateTime>(nullable: true),
                    Fuente = table.Column<string>(maxLength: 32, nullable: true, defaultValue: "CP1053"),
                    FuenteImport = table.Column<string>(maxLength: 32, nullable: true),
                    Proceso = table.Column<int>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "CURRENT_USER")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personajes", x => x.ColMagPersonajeId);
                });

            migrationBuilder.CreateTable(
                name: "VaritaMagica",
                schema: "ColMag",
                columns: table => new
                {
                    ColMagPersonajeId = table.Column<int>(nullable: false),
                    ColMagVaritaMagicaId = table.Column<int>(nullable: false),
                    ColMagComp = table.Column<string>(nullable: true),
                    Madera = table.Column<string>(maxLength: 20, nullable: true),
                    ColMagVaritaMagicaAlma = table.Column<string>(maxLength: 22, nullable: true),
                    ColMagVaritaMagicaLongitud = table.Column<int>(nullable: false),
                    Fecha_Computador = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Fuente = table.Column<string>(maxLength: 32, nullable: true, defaultValue: "CP1053"),
                    FuenteImport = table.Column<string>(maxLength: 32, nullable: true),
                    Proceso = table.Column<int>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "CURRENT_USER")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaritaMagica", x => new { x.ColMagPersonajeId, x.ColMagVaritaMagicaId });
                    table.ForeignKey(
                        name: "FK_VaritaMagica_Personajes_ColMagPersonajeId",
                        column: x => x.ColMagPersonajeId,
                        principalSchema: "ColMag",
                        principalTable: "Personajes",
                        principalColumn: "ColMagPersonajeId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaritaMagica",
                schema: "ColMag");

            migrationBuilder.DropTable(
                name: "Personajes",
                schema: "ColMag");
        }
    }
}
