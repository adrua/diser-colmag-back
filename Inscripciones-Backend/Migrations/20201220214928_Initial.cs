using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inscripciones_Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "COLMAG");

            migrationBuilder.CreateTable(
                name: "Casas",
                schema: "COLMAG",
                columns: table => new
                {
                    ColmagCasaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColmagCasaNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColmagCasaDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Computador = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    Fecha_Impresion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fecha_Reimpresion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fuente = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true, defaultValue: "CP1050"),
                    FuenteImport = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Proceso = table.Column<int>(type: "int", nullable: true),
                    Usuario = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "CURRENT_USER")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casas", x => x.ColmagCasaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casas",
                schema: "COLMAG");
        }
    }
}
