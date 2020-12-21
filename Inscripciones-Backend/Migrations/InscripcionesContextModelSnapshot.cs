﻿// <auto-generated />
using System;
using Inscripciones.TablasBasicas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inscripciones_Backend.Migrations
{
    [DbContext(typeof(InscripcionesContext))]
    partial class InscripcionesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Inscripciones.TablasBasicas.Models.ColmagCasas", b =>
                {
                    b.Property<int>("ColmagCasaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ColmagCasaDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColmagCasaNombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasDefaultValue("CP1050");

                    b.Property<string>("FuenteImport")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("Proceso")
                        .HasColumnType("int");

                    b.Property<string>("Usuario")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValueSql("CURRENT_USER");

                    b.HasKey("ColmagCasaId");

                    b.ToTable("Casas", "COLMAG");
                });
#pragma warning restore 612, 618
        }
    }
}