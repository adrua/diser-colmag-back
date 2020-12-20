//ColmagCasasModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;

namespace Inscripciones.TablasBasicas.Models
{
    
    [Table("Casas", Schema = "COLMAG")]
    public class ColmagCasas
	{	
        public int ColmagCasaId {get; set;}

        [MaxLength(50)]
        public string ColmagCasaNombre {get; set;}

        public string ColmagCasaDescripcion {get; set;}
	}

    #region Enum
    #endregion

	
    /// <summary>
    /// Extensión para registrar mapping con el Entity Framework y oData
    /// </summary>
	public static class ColmagCasasExtension
	{	        
        #region EF Mapping
        public static ModelBuilder ColmagCasasMapping(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ColmagCasas>();
            
            //PrimaryKey
            entity.HasKey(c => new { c.ColmagCasaId });

           //Relationships
            //Shadow Properties
            entity.Property<string>("Fuente").HasMaxLength(32).HasDefaultValue("CP1050");
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
        public static void ColmagCasasMapping(this ODataConventionModelBuilder oDataModelBuilder)
        {
            var entityConfig = oDataModelBuilder.EntitySet<ColmagCasas>(nameof(ColmagCasas));

            var entity = entityConfig.EntityType;

            // PrimaryKey
            entity.HasKey(c => new { c.ColmagCasaId });


            ////Ignored properties for oData
            // entityConfig.EntityType.Ignore(x => x.Summary);
        }		
        #endregion
    }
}
