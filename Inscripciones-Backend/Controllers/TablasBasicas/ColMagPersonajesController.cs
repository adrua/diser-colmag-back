//ColMagPersonajesController.cs
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
    public class ColMagPersonajesController : ODataController
	{	
        private readonly ILogger<ColMagPersonajesController> logger;
        private readonly IColMagPersonajesManager ColMagPersonajesManager;

        public ColMagPersonajesController(ILogger<ColMagPersonajesController> logger,
                                    IColMagPersonajesManager ColMagPersonajesManager)
        {
            this.logger = logger;
            this.ColMagPersonajesManager = ColMagPersonajesManager;
        }

        //[HttpGet]
        [EnableQuery]
        public IEnumerable<ColMagPersonajes> Get()
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return this.ColMagPersonajesManager.GetAll();
        }

        [HttpGet("(ColMagPersonajeId={keyColMagPersonajeId})")]
        [EnableQuery]
        public IActionResult Get([FromODataUri] int keyColMagPersonajeId)
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            var row = this.ColMagPersonajesManager.GetById(keyColMagPersonajeId);

            if (row == null)
            {
                return BadRequest($"Error buscando, Fila no existe.");
            }

            return Ok(row);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ColMagPersonajes row)
        {
        
            try
            {
                var orgrow = this.ColMagPersonajesManager.Add(row);
                if (orgrow == null)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    return BadRequest($"Llave primaria duplicada ({row.ColMagPersonajeId})");
                }
                else
                {
                    this.ColMagPersonajesManager.SaveChanges();
                    return Created(row);
                }
            }
            catch(Exception ex)
            {
                var errors = String.Join("\n", ModelState.Root.Errors.Select((e) => e.Exception.Message));
                return BadRequest($"Código repetido o datos inválidos\n{errors}");
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromODataUri] int keyColMagPersonajeId, Delta<ColMagPersonajes> changes)
        {
            try
            {
                var row = this.ColMagPersonajesManager.Update(keyColMagPersonajeId, changes);
                if (row == null)
                {
                    return BadRequest($"Error actualizando, Fila no existe.");
                }
                else
                {
                    this.ColMagPersonajesManager.SaveChanges();
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
        public IActionResult Delete([FromODataUri] int keyColMagPersonajeId)
        {
            try
            {
                var row = this.ColMagPersonajesManager.Delete(keyColMagPersonajeId);
                if (row == null)
                {
                    return BadRequest($"Error eliminando, Fila no existe.");
                }
                else
                {
                    this.ColMagPersonajesManager.SaveChanges();
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
