//ColMagVaritaMagicaController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Inscripciones.TablasBasicas.Interfaces;
using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.DataLayer.Models
{
    //[Route("odata/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = false)]
    [Authorize(Policy = "Bearer")]
    public class ColMagVaritaMagicaController : ODataController
	{	
        private readonly ILogger<ColMagVaritaMagicaController> logger;
        private readonly IColMagVaritaMagicaManager ColMagVaritaMagicaManager;

        public ColMagVaritaMagicaController(ILogger<ColMagVaritaMagicaController> logger,
                                    IColMagVaritaMagicaManager ColMagVaritaMagicaManager)
        {
            this.logger = logger;
            this.ColMagVaritaMagicaManager = ColMagVaritaMagicaManager;
        }

        //[HttpGet]
        [EnableQuery]
        public IEnumerable<ColMagVaritaMagica> Get()
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return this.ColMagVaritaMagicaManager.GetAll();
        }

        [HttpGet("(ColMagPersonajeId={keyColMagPersonajeId}, ColMagVaritaMagicaId={keyColMagVaritaMagicaId})")]
        [EnableQuery]
        public IActionResult Get([FromODataUri] int keyColMagPersonajeId, [FromODataUri] int keyColMagVaritaMagicaId)
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            var row = this.ColMagVaritaMagicaManager.GetById(keyColMagPersonajeId, keyColMagVaritaMagicaId);

            if (row == null)
            {
                return BadRequest($"Error buscando, Fila no existe.");
            }

            return Ok(row);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ColMagVaritaMagica row)
        {
        
            try
            {
                var orgrow = this.ColMagVaritaMagicaManager.Add(row);
                if (orgrow == null)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    return BadRequest($"Llave primaria duplicada ({row.ColMagPersonajeId}, {row.ColMagVaritaMagicaId})");
                }
                else
                {
                    this.ColMagVaritaMagicaManager.SaveChanges();
                    return Created(row);
                }
            }
            catch(Exception)
            {
                var errors = String.Join("\n", ModelState.Root.Errors.Select((e) => e.Exception.Message));
                return BadRequest($"Código repetido o datos inválidos\n{errors}");
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromODataUri] int keyColMagPersonajeId, [FromODataUri] int keyColMagVaritaMagicaId, Delta<ColMagVaritaMagica> changes)
        {
            try
            {
                var row = this.ColMagVaritaMagicaManager.Update(keyColMagPersonajeId, keyColMagVaritaMagicaId, changes);
                if (row == null)
                {
                    return BadRequest($"Error actualizando, Fila no existe.");
                }
                else
                {
                    this.ColMagVaritaMagicaManager.SaveChanges();
                    return Updated(row);
                }
            }
            catch(Exception)
            {
                var errors = String.Join("\n", ModelState.Root.Errors.Select((e) => e.Exception.Message));
                return BadRequest($"Código repetido o datos inválidos\n{errors}");
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int keyColMagPersonajeId, [FromODataUri] int keyColMagVaritaMagicaId)
        {
            try
            {
                var row = this.ColMagVaritaMagicaManager.Delete(keyColMagPersonajeId, keyColMagVaritaMagicaId);
                if (row == null)
                {
                    return BadRequest($"Error eliminando, Fila no existe.");
                }
                else
                {
                    this.ColMagVaritaMagicaManager.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest($"Error eliminando, el registro está en uso.");
            }
        }
    }
}
