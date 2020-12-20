using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace Inscripciones.OData
{
    public class ConventionModelBuilder
    {
        public ConventionModelBuilder(ODataConventionModelBuilder modelBuilder) 
        {
            modelBuilder.ColmagCasasMapping();
        }

        public static void AddODataScoped(IServiceCollection 