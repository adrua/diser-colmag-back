using Microsoft.Extensions.DependencyInjection;
using Inscripciones.TablasBasicas.Models;
using Inscripciones.TablasBasicas.Interfaces;
using Inscripciones.TablasBasicas.Managers;
using Microsoft.AspNet.OData.Builder;
using Inscripciones.Procesos.Models;
using Inscripciones.Procesos.Interfaces;
using Inscripciones.Procesos.Managers;

namespace Inscripciones.OData
{
    public class ConventionModelBuilder
    {
        public ConventionModelBuilder(ODataConventionModelBuilder modelBuilder) 
        {
            modelBuilder.ColmagCasasMapping();
            modelBuilder.ColmagInscripcionesMapping();
            modelBuilder.ColMagPersonajesMapping();
            modelBuilder.ColMagVaritaMagicaMapping();
        }

        public static void AddODataScoped(IServiceCollection services) 
        {
            services.AddScoped<IColmagCasasManager, ColmagCasasManager>();
            services.AddScoped<IColmagInscripcionesManager, ColmagInscripcionesManager>();
            services.AddScoped<IColMagPersonajesManager, ColMagPersonajesManager>();
            services.AddScoped<IColMagVaritaMagicaManager, ColMagVaritaMagicaManager>();
        }
    } 
}

