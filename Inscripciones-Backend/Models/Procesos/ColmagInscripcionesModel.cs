//ColmagInscripcionesModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inscripciones.TablasBasicas.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.EntityFrameworkCore;

namespace Inscripciones.Procesos.Models
{
    
    [Table("Inscripciones", Schema = "COLMAG")]
    public class ColmagInscripciones
	{	
        public int ColmagInscripcionId {get; set;}

        //[Column(TypeName = "Date")]
        public DateTime ColmagInscripcionFecha {get; set;}

        [MaxLength(20)]
        public string ColmagInscripcionNombre {get; set;}

        [MaxLength(20)]
        public string ColmagInscripcionApellido {get; set;}

        [Column(TypeName = "decimal(16, 0)")]
        public decimal ColmagInscripcionCedula {get; set;}

        public int ColmagInscripcionEdad {get; set;}

        public int ColmagCasaId {get; set;}

        public ColmagCasas ColmagCasas { get; set; }
	}

    #region Enum
    #endregion

    #region EF Mapping
	
    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
	public static class ColmagInscripcionesExtension
	{	        
        public static ModelBuilder ColmagInscripcionesMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ColmagInscripciones>();
            
            //PrimaryKey
            entity.HasKey(c => new { c.ColmagInscripcionId });
            entity.HasIndex(u => u.ColmagInscripcionCedula).IsUnique();

           //Relationships
            entity.HasOne(typeof(ColmagCasas), "ColmagCasas")
                .WithMany()
                .HasForeignKey("ColmagCasaId")
                .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
                
            //Shadow Properties
            entity.Property<string>("Fuente").HasMaxLength(32).HasDefaultValue("CP1090");
            entity.Property<string>("FuenteImport").HasMaxLength(32);
            entity.Property<int?>("Proceso");
            entity.Property<DateTime?>("Fecha_Computador").HasDefaultValueSql("getdate()");
            entity.Property<string>("Usuario").HasMaxLength(255).HasDefaultValueSql("CURRENT_USER");
            entity.Property<DateTime?>("Fecha_Impresion");
            entity.Property<DateTime?>("Fecha_Reimpresion");

			return modelBuilder;
		}
		
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ColmagInscripcionesMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ColmagInscripciones>(nameof(ColmagInscripciones));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.ColmagInscripcionId });


            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
		
    }
    #endregion
}
