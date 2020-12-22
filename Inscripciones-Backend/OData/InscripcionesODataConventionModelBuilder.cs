using Microsoft.AspNet.OData.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Inscripciones.OData
{
    public class InscripcionesODataConventionModelBuilder : ODataConventionModelBuilder
    {
        public ConventionModelBuilder _general;

        public InscripcionesODataConventionModelBuilder(IServiceProvider provider) : base(provider)
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
