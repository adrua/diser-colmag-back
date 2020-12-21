using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;
using System;

namespace Inscripciones.OData
{
    public class InscripcionesODataConventionModelBuilder : ODataConventionModelBuilder
    {
        public ConventionModelBuilder _general;

        public InscripcionesODataConventionModelBuilder() : base()
        {
            //oData Entity Mapping
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
