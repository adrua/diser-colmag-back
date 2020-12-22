//ColMagVaritaMagicaModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;
using Microsoft.EntityFrameworkCore;

namespace Inscripciones.TablasBasicas.Models
{
    
    [Table("VaritaMagica", Schema = "ColMag")]
    public class ColMagVaritaMagica
	{	
        public int ColMagPersonajeId {get; set;}

        public int ColMagVaritaMagicaId {get; set;}

        public string ColMagComp  
        {
        	get { return $"{ColMagPersonajeId}/{ColMagVaritaMagicaId}"; } 
        	private set {}
        }

        [MaxLength(20)]
        public string ColMagVaritaMagicaMadera {get; set;}

        [MaxLength(22)]
        public string ColMagVaritaMagicaAlma {get; set;}

        [Column(TypeName = "decimal(5, 2)")]
        public decimal ColMagVaritaMagicaLongitud {get; set;}
	}

    #region Enum
    #endregion

    #region EF Mapping
	
    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
	public static class ColMagVaritaMagicaExtension
	{	        
        public static ModelBuilder ColMagVaritaMagicaMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ColMagVaritaMagica>();
            
            //PrimaryKey
            entity.HasKey(c => new { c.ColMagPersonajeId, c.ColMagVaritaMagicaId });

           //Relationships
            //Shadow Properties
            entity.Property<string>("Fuente").HasMaxLength(32).HasDefaultValue("CP1053");
            entity.Property<string>("FuenteImport").HasMaxLength(32);
            entity.Property<int?>("Proceso");
            entity.Property<DateTime?>("Fecha_Computador").HasDefaultValueSql("getdate()");
            entity.Property<string>("Usuario").HasMaxLength(255).HasDefaultValueSql("CURRENT_USER");

			return modelBuilder;
		}
		
        /// <summary>
        /// Mapping con el oData Framework
        /// </summary>
        /// <param name="oDataModelBuilder"></param>
        public static void ColMagVaritaMagicaMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ColMagVaritaMagica>(nameof(ColMagVaritaMagica));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.ColMagPersonajeId, c.ColMagVaritaMagicaId });


            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }
		
    }
    #endregion
}
