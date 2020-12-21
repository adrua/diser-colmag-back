using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.Extensions.DependencyInjection;
using Inscripciones.TablasBasicas.Models;
using Inscripciones.TablasBasicas.Interfaces;
using Inscripciones.TablasBasicas.Managers;

namespace Inscripciones.OData
{
    public class ConventionModelBuilder
    {
        public ConventionModelBuilder(ODataConventionModelBuilder modelBuilder) 
        {
            modelBuilder.ColmagCasasMapping();
        }

        public static void AddODataScoped(IServiceCollection services) 
        {
            services.AddScoped<IColmagCasasManager, ColmagCasasManager>();
        }
    } 
}

