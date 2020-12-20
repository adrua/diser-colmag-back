//ColmagCasasController.cs
using System.Threading;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Logging;

using Inscripciones.TablasBasicas.Interfaces;
using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.DataLayer.Models
{
    [Authorize(Policy = "Bearer")]
    public class ColmagCasasController : ODataController
	{	
        private readonly ILogger<ColmagCasasController> logger;
        private readonly IColmagCasasManager ColmagCasasManager;

        public ColmagCasasController(ILogger<ColmagCasasController> logger,
                                    IColmagCasasManager ColmagCasasManager)
        {
            this.logger = logger;
            this.ColmagCasasManager = ColmagCasasManager;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get(CancellationToken token)
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return Ok(this.ColmagCasasManager.GetAll());
        }

        [HttpGet("(COLMAGCasaId={keyColmagCasaId})")]
        [EnableQuery]
        public IActionResult Get([FromODataUri] int keyColmagCasaId)
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            var row = this.ColmagCasasManager.GetById(keyColmagCasaId);

            if (row == null)
            {
                return BadRequest($"Error buscando, Fila no existe.");
            }

            return Ok(row);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ColmagCasas row, CancellationToken token)
        {
        
            var orgrow = this.ColmagCasasManager.Add(row);
            if (orgrow == null)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return BadRequest($"Llave primaria duplicada ({row.ColmagCasaId})");
            }
            else
            {
                this.ColmagCasasManager.SaveChanges();
                return Created(row);
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromODataUri] int keyColmagCasaId, [FromBody]Delta<ColmagCasas> changes)
        {
            var row = this.ColmagCasasManager.Update(keyColmagCasaId, changes);
            if (row == null)
            {
                return BadRequest($"Error actualizando, Fila no existe.");
            }
            else
            {
                this.ColmagCasasManager.SaveChanges();
                return Updated(row);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int keyColmagCasaId)
        {
            var row = this.ColmagCasasManager.Delete(keyColmagCasaId);
            if (row == null)
            {
                return BadRequest($"Error eliminando, Fila no existe.");
            }
            else
            {
                this.ColmagCasasManager.SaveChanges();
                return Ok();
            }
        }
    }
}
