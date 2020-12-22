//ColMagPersonajesModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;
using Microsoft.EntityFrameworkCore;

namespace Inscripciones.TablasBasicas.Models
{
    
    [Table("Personajes", Schema = "ColMag")]
    public class ColMagPersonajes
	{	
        public int ColMagPersonajeId {get; set;}

        [MaxLength(75)]
        public string ColMagPersonajeNombre {get; set;}

        [MaxLength(20)]
        public string ColMagPersonajeEspecie {get; set;}

        [MaxLength(10)]
        public string Genero {get; set;}

        public int ColmagCasaId {get; set;}

        [Column(TypeName = "Date")]
        public DateTime? ColMagPersonajeFechaNacimiento {get; set;}

        public int? ColMagPersonajeAnoNacimiento {get; set;}

        [MaxLength(29)]
        public string ColMagPersonajeAscendencia {get; set;}

        [MaxLength(20)]
        public string ColMagPersonajeColorOjos {get; set;}

        [MaxLength(20)]
        public string ColMagPersonajeColorCabello {get; set;}

        [MaxLength(30)]
        public string ColMagPersonajePatronus {get; set;}

        [Column(TypeName = "bit")]
        public bool ColMagPersonajeEstudiante {get; set;}

        [Column(TypeName = "bit")]
        public bool ColMagPersonajeProfesor {get; set;}

        [MaxLength(75)]
        public string ColMagPersonajeActor {get; set;}

        [Column(TypeName = "bit")]
        public bool ColMagPersonajeVive {get; set;}

        [MaxLength(250)]
        public string ColMagPersonajeImagen {get; set;}

        public ColmagCasas ColmagCasas { get; set; }

        public ICollection<ColMagVaritaMagica> ColMagVaritaMagica { get; set; }
	}

    #region Enum
    #endregion

    #region EF Mapping
	
    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
	public static class ColMagPersonajesExtension
	{	        
        public static ModelBuilder ColMagPersonajesMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ColMagPersonajes>();
            
            //PrimaryKey
            entity.HasKey(c => new { c.ColMagPersonajeId });

           //Relationships
            entity.HasOne(typeof(ColmagCasas), "ColmagCasas")
                .WithMany()
                .HasForeignKey("ColmagCasaId")
                .OnDelete(DeleteBehavior.Restrict); // no ON DELETE
                
            //Shadow Properties
            entity.Property<string>("Fuente").HasMaxLength(32).HasDefaultValue("CP1053");
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
        public static void ColMagPersonajesMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ColMagPersonajes>(nameof(ColMagPersonajes));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.ColMagPersonajeId });
            entity.Property(c => c.ColMagPersonajeFechaNacimiento).AsDate();

            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
		
    }
    #endregion
}
