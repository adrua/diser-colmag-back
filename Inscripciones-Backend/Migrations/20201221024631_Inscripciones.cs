using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class Inscripciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inscripciones",
                schema: "COLMAG",
                columns: table => new
                {
                    ColmagInscripcionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColmagInscripcionFecha = table.Column<DateTime>(nullable: false),
                    ColmagInscripcionNombre = table.Column<string>(maxLength: 20, nullable: true),
                    ColmagInscripcionApellido = table.Column<string>(maxLength: 20, nullable: true),
                    ColmagInscripcionCedula = table.Column<decimal>(type: "decimal(16, 0)", nullable: false),
                    ColmagInscripcionEdad = table.Column<int>(nullable: false),
                    ColmagCasaId = table.Column<int>(nullable: false),
                    Fecha_Computador = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Fecha_Impresion = table.Column<DateTime>(nullable: true),
                    Fecha_Reimpresion = table.Column<DateTime>(nullable: true),
                    Fuente = table.Column<string>(maxLength: 32, nullable: true, defaultValue: "CP1090"),
                    FuenteImport = table.Column<string>(maxLength: 32, nullable: true),
                    Proceso = table.Column<int>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "CURRENT_USER")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.ColmagInscripcionId);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Casas_ColmagCasaId",
                        column: x => x.ColmagCasaId,
                        principalSchema: "COLMAG",
                        principalTable: "Casas",
                        principalColumn: "ColmagCasaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ColmagCasaId",
                schema: "COLMAG",
                table: "Inscripciones",
                column: "ColmagCasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ColmagInscripcionCedula",
                schema: "COLMAG",
                table: "Inscripciones",
                column: "ColmagInscripcionCedula",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscripciones",
                schema: "COLMAG");
        }
    }
}
