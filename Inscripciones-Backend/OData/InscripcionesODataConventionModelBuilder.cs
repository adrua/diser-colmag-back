using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;

namespace Inscripciones.OData
{
    public class InscripcionesODataConventionModelBuilder : ODataConventionModelBuilder
    {
        public ConventionModelBuilder _general;

        public IServiceCollection Services { get; set; }

        public InscripcionesODataConventionModelBuilder(IServiceCollection services) : base()
        {
            //oData Entity Mapping
            Services = services;
            _general = new ConventionModelBuilder(this);
        }

    }

    public static class extensions 
    {
        public static void AddODataScoped(this IServiceCollection services) 
        {
            ConventionModelBuilder.AddODataScoped(services);
        }

    } 
}
