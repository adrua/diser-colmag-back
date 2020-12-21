using Inscripciones.OData;
using Inscripciones.TablasBasicas.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;

namespace Inscripciones_Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Configure the EntityFramework context
            services.AddDbContext<InscripcionesContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost", "https://localhost")
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()
                                                                     .AllowCredentials()));

            services.AddControllers();

            services.AddHttpContextAccessor();

            //Managers
            services.AddODataScoped();

            var builder = new InscripcionesODataConventionModelBuilder();
            IEdmModel model0 = builder.GetEdmModel();

            var oDataBuilder = services.AddOData(opt =>
            {
                opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(100)
                .AddModel("odata", model0)
                //.AddModel("v1", model1)
                //.AddModel("v2{data}", model2, builder => builder.AddService<ODataBatchHandler, DefaultODataBatchHandler>(Microsoft.OData.ServiceLifetime.Singleton))
                ;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inscripciones_Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inscripciones_Backend v1"));
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<InscripcionesContext>();
            //    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            //    {
            //        context.Database.Migrate();
            //    }
            //}
        }
    }
}
