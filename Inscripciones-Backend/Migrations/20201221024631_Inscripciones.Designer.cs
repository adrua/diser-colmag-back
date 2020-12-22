﻿// <auto-generated />
using System;
using Inscripciones.TablasBasicas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inscripciones_Backend.Migrations
{
    [DbContext(typeof(ColMagContext))]
    [Migration("20201221024631_Inscripciones")]
    partial class Inscripciones
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Inscripciones.Procesos.Models.ColmagInscripciones", b =>
                {
                    b.Property<int>("ColmagInscripcionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColmagCasaId")
                        .HasColumnType("int");

                    b.Property<string>("ColmagInscripcionApellido")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<decimal>("ColmagInscripcionCedula")
                        .HasColumnType("decimal(16, 0)");

                    b.Property<int>("ColmagInscripcionEdad")
                        .HasColumnType("int");

                    b.Property<DateTime>("ColmagInscripcionFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("ColmagInscripcionNombre")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<DateTime?>("Fecha_Computador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Fecha_Impresion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Fecha_Reimpresion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fuente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32)
                        .HasDefaultValue("CP1090");

                    b.Property<string>("FuenteImport")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int?>("Proceso")
                        .HasColumnType("int");

                    b.Property<string>("Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("CURRENT_USER")
                        .HasMaxLength(255);

                    b.HasKey("ColmagInscripcionId");

                    b.HasIndex("ColmagCasaId");

                    b.HasIndex("ColmagInscripcionCedula")
                        .IsUnique();

                    b.ToTable("Inscripciones","COLMAG");
                });

            modelBuilder.Entity("Inscripciones.TablasBasicas.Models.ColmagCasas", b =>
                {
                    b.Property<int>("ColmagCasaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColmagCasaDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColmagCasaNombre")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Fecha_Computador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("Fecha_Impresion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Fecha_Reimpresion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fuente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32)
                        .HasDefaultValue("CP1050");

                    b.Property<string>("FuenteImport")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int?>("Proceso")
                        .HasColumnType("int");

                    b.Property<string>("Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("CURRENT_USER")
                        .HasMaxLength(255);

                    b.HasKey("ColmagCasaId");

                    b.ToTable("Casas","COLMAG");
                });

            modelBuilder.Entity("Inscripciones.Procesos.Models.ColmagInscripciones", b =>
                {
                    b.HasOne("Inscripciones.TablasBasicas.Models.ColmagCasas", "ColmagCasas")
                        .WithMany()
                        .HasForeignKey("ColmagCasaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}