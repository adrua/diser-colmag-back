using System;
using System.Linq;
using Inscripciones.TablasBasicas.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Inscripciones_Backend.Testing
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ColMagContext Db;
        public Utilities utilities;
        public ILogger<CustomWebApplicationFactory<TStartup>> logger;
        public string Token;
        public Action<ColMagContext> InitializeDbForTests = null;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            utilities = new Utilities();
            Token = utilities.Login();

            builder.ConfigureServices(services =>
            {

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ColMagContext>));

                services.Remove(descriptor);

                services.AddDbContext<ColMagContext>(options =>
                {
                    // options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Initial Catalog=esap_testing;AttachDbFileName=C:\\Solin\\Esap-Data\\esap_testing.mdf;Integrated Security=true;MultipleActiveResultSets=true");
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {

                    var scopedServices = scope.ServiceProvider;
                    Db = scopedServices.GetRequiredService<ColMagContext>();
                    logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    Db.Database.EnsureCreated();

                    InitializeDbForTests(Db);

                }

            });
        }

    }
}
