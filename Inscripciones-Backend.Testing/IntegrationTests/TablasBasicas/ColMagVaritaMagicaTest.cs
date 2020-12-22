// ColMagVaritaMagica - APIRest - OData Testing
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Xunit;

using Inscripciones.TablasBasicas.Models;
using Inscripciones_Backend;
using Inscripciones_Backend.Testing;

namespace Inscripciones.TablasBasicas.Tests.IntegrationTests
{
    public class ColMagVaritaMagicaTest
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        #region Tests
        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1234, ColMagVaritaMagicaId=1234)")]
        public async Task GetById_Ok(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            // Act
            var response = await client.GetAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=-1, ColMagVaritaMagicaId=-1)")]
        public async Task GetById_NotFound(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await client.GetAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Bad Request", response.ReasonPhrase);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Assert.StartsWith("Error buscando, Fila no existe.", json.value.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1234, ColMagVaritaMagicaId=1234)")]
        public async Task GetById_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);

            Assert.Null(response.Content.Headers.ContentType);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica")]
        public async Task Get(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await client.GetAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Assert.True(json.value.Count >= 1);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica")]
        public async Task Get_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);

            Assert.Null(response.Content.Headers.ContentType);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica")]
        public async Task Post_OK(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var contentJson = JsonConvert.SerializeObject(rowAdd, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PostAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica")]
        public async Task Post_Duplicate(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var contentJson = JsonConvert.SerializeObject(rowBase, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PostAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Bad Request", response.ReasonPhrase);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Assert.StartsWith("Llave primaria duplicada (", json.value.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica")]
        public async Task Post_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            var contentJson = JsonConvert.SerializeObject(rowAdd, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);

            Assert.Null(response.Content.Headers.ContentType);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1234, ColMagVaritaMagicaId=1234)")]
        public async Task Patch_OK(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var contentJson = JsonConvert.SerializeObject(rowBase, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PatchAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal("No Content", response.ReasonPhrase);

            Assert.True(response.Content.Headers.ContentType == null);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=-1, ColMagVaritaMagicaId=-1)")]
        public async Task Patch_NotFound(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var contentJson = JsonConvert.SerializeObject(rowBase, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PatchAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Bad Request", response.ReasonPhrase);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Assert.Equal("Error actualizando, Fila no existe.", json.value.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1234, ColMagVaritaMagicaId=1234)")]
        public async Task Patch_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var contentJson = JsonConvert.SerializeObject(rowBase, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>() { new Newtonsoft.Json.Converters.StringEnumConverter() }
            });
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PatchAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);

            Assert.Null(response.Content.Headers.ContentType);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1234, ColMagVaritaMagicaId=1234)")]
        public async Task Put_NotImplemented(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var contentJson = JsonConvert.SerializeObject(rowBase, Formatting.None, Utilities.JsonSerializerSettings);
            var content = new StringContent(contentJson, ASCIIEncoding.UTF8, "application/json");
            var response = await client.PutAsync($"{Utilities.BaseAddress}{url}", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("Not Found", response.ReasonPhrase);

            Assert.True(response.Content.Headers.ContentType == null);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1236,ColMagVaritaMagicaId=1236)")]
        public async Task Delete_OK(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await client.DeleteAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.True(response.Content.Headers.ContentType == null);
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=-1, ColMagVaritaMagicaId=-1)")]
        public async Task Delete_NotFound(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await client.DeleteAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("Bad Request", response.ReasonPhrase);

            Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true", response.Content.Headers.ContentType.ToString());
            dynamic json = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Assert.Equal("Error eliminando, Fila no existe.", json.value.ToString());
        }

        [Theory]
        [InlineData("/odata/ColMagVaritaMagica(ColMagPersonajeId=1236,ColMagVaritaMagicaId=1236)")]
        public async Task Delete_Unauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"{Utilities.BaseAddress}{url}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("Unauthorized", response.ReasonPhrase);

            Assert.Null(response.Content.Headers.ContentType);
            Assert.Equal("", response.Content.ReadAsStringAsync().Result);
        }
        #endregion

        #region Setup
        Utilities Utilities = null;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private string _token;

        private static ColMagVaritaMagica rowBase = new ColMagVaritaMagica() {
            ColMagPersonajeId = 1234,
            ColMagVaritaMagicaId = 1234,
            ColMagVaritaMagicaMadera = $@"holly",
            ColMagVaritaMagicaAlma = $@"phoenix feather",
            ColMagVaritaMagicaLongitud = 11.00m,
        };

        private static ColMagVaritaMagica rowAdd = new ColMagVaritaMagica() {
            ColMagPersonajeId = 1235,
            ColMagVaritaMagicaId = 1235,
            ColMagVaritaMagicaMadera = $@"iolly",
            ColMagVaritaMagicaAlma = $@"qhoenix feather",
            ColMagVaritaMagicaLongitud = 12.00m,
        };

        private static ColMagVaritaMagica rowDelete = new ColMagVaritaMagica() {
            ColMagPersonajeId = 1236,
            ColMagVaritaMagicaId = 1236,
            //ColMagVaritaMagicaComp = '', //convert(varchar(max),ColMagPersonajeId) || '/' || convert(varchar(max),ColMagVaritaMagicaId) 
            ColMagVaritaMagicaMadera = $@"jolly",
            ColMagVaritaMagicaAlma = $@"rhoenix feather",
            ColMagVaritaMagicaLongitud = 13.00m,
        };

        public ColMagVaritaMagicaTest(CustomWebApplicationFactory<Startup> factory)
        {

            _factory = factory;

            if (factory.Db == null)
            {
                _factory.InitializeDbForTests = (db) =>
                {
                    if (db.ColMagVaritaMagica.Count() == 0)
                    {
                        db.ColMagVaritaMagica.Add(rowBase);
                        db.ColMagVaritaMagica.Add(rowDelete);
                        db.SaveChanges();
                    }

                    Utilities = _factory.utilities;
                    _token = _factory.Token;
                };
            }
            else
            {
                Utilities = _factory.utilities;
                _token = _factory.Token;
            }
        }
        #endregion
    }
}