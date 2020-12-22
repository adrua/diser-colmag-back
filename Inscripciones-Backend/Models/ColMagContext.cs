using Inscripciones.Procesos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inscripciones.TablasBasicas.Models
{
    public class ColMagContext
                    : DbContext
    {


        private readonly IConfiguration Configuration;
        public DbSet<ColmagCasas> ColmagCasas { get; set; }
        public DbSet<ColmagInscripciones> ColmagInscripciones { get; set; }
        public DbSet<ColMagPersonajes> ColMagPersonajes { get; set; }
        public DbSet<ColMagVaritaMagica> ColMagVaritaMagica { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ColmagCasasMapping();
            modelBuilder.ColmagInscripcionesMapping();
            modelBuilder.ColMagPersonajesMapping();
            modelBuilder.ColMagVaritaMagicaMapping();
        }

        // Use only for ArkeosFactory Test. Please remove on Application
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning));
        }

        // Use only for ArkeosFactory Test. Please remove on Application
        public ColMagContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

    }
}

