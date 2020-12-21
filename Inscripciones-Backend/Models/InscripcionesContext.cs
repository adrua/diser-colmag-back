using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Inscripciones.TablasBasicas.Models
{
    public class InscripcionesContext
                    : DbContext
    {


        private readonly IConfiguration Configuration;
        public DbSet<ColmagCasas> ColmagCasas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ColmagCasasMapping();

        }

        // Use only for ArkeosFactory Test. Please remove on Application
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning));
        }

        // Use only for ArkeosFactory Test. Please remove on Application
        public InscripcionesContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

    }
}

