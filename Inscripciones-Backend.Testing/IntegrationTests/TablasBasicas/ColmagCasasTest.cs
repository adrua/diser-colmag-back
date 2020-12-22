// ColmagCasas - APIRest - OData Testing
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
    public class ColmagCasasTest
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        #region Tests
        [Theory]
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1234)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=-1)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1234)")]
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
        [InlineData("/odata/ColmagCasas")]
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
        [InlineData("/odata/ColmagCasas")]
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
        [InlineData("/odata/ColmagCasas")]
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
        [InlineData("/odata/ColmagCasas")]
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
        [InlineData("/odata/ColmagCasas")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1234)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=-1)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1234)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1234)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1236)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=-1)")]
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
        [InlineData("/odata/ColmagCasas(COLMAGCasaId=1236)")]
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

        private static ColmagCasas rowBase = new ColmagCasas() {
            ColmagCasaId = 1234,
            ColmagCasaNombre = $@"Griffindor",
            ColmagCasaDescripcion = $@"Lorem ipsum dolor sit amet. consectetur adipiscing elit. sed do eiusmod tempor incididunt
ut
labore
et
dolore
magna
aliqua.
Ut
enim
ad
minim
veniam.
quis
nostrud
exercitation
ullamco
laboris
nisi
ut
aliquip
ex
ea
commodo
consequat.
Duis
aute
irure
dolor
in
reprehenderit
in
voluptate
velit
esse
cillum
dolore
eu
fugiat
nulla
pariatur.
Excepteur
sint
occaecat
cupidatat
non
proident.
sunt
in
culpa
qui
officia
deserunt
mollit
anim
id
est
laborum.",
        };

        private static ColmagCasas rowAdd = new ColmagCasas() {
            ColmagCasaId = 1235,
            ColmagCasaNombre = $@"Hriffindor",
            ColmagCasaDescripcion = $@"Morem ipsum dolor sit amet. consectetur adipiscing elit. sed do eiusmod tempor incididunt
ut
labore
et
dolore
magna
aliqua.
Ut
enim
ad
minim
veniam.
quis
nostrud
exercitation
ullamco
laboris
nisi
ut
aliquip
ex
ea
commodo
consequat.
Duis
aute
irure
dolor
in
reprehenderit
in
voluptate
velit
esse
cillum
dolore
eu
fugiat
nulla
pariatur.
Excepteur
sint
occaecat
cupidatat
non
proident.
sunt
in
culpa
qui
officia
deserunt
mollit
anim
id
est
laborum.",
        };

        private static ColmagCasas rowDelete = new ColmagCasas() {
            ColmagCasaId = 1236,
            ColmagCasaNombre = $@"Iriffindor",
            ColmagCasaDescripcion = $@"Norem ipsum dolor sit amet. consectetur adipiscing elit. sed do eiusmod tempor incididunt
ut
labore
et
dolore
magna
aliqua.
Ut
enim
ad
minim
veniam.
quis
nostrud
exercitation
ullamco
laboris
nisi
ut
aliquip
ex
ea
commodo
consequat.
Duis
aute
irure
dolor
in
reprehenderit
in
voluptate
velit
esse
cillum
dolore
eu
fugiat
nulla
pariatur.
Excepteur
sint
occaecat
cupidatat
non
proident.
sunt
in
culpa
qui
officia
deserunt
mollit
anim
id
est
laborum.",
        };

        public ColmagCasasTest(CustomWebApplicationFactory<Startup> factory)
        {

            _factory = factory;

            if (factory.Db == null)
            {
                _factory.InitializeDbForTests = (db) =>
                {
                    if (db.ColmagCasas.Count() == 0)
                    {
                        db.ColmagCasas.Add(rowBase);
                        db.ColmagCasas.Add(rowDelete);
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