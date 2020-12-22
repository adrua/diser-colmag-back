//ColmagInscripcionesController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Inscripciones.Procesos.Interfaces;
using Inscripciones.Procesos.Models;

namespace Inscripciones.Procesos.DataLayer.Models
{
    //[Route("odata/[controller]")]
    //[ApiExplorerSettings(IgnoreApi = false)]
    [Authorize(Policy = "Bearer")]
    public class ColmagInscripcionesController : ODataController
	{	
        private readonly ILogger<ColmagInscripcionesController> logger;
        private readonly IColmagInscripcionesManager ColmagInscripcionesManager;

        public ColmagInscripcionesController(ILogger<ColmagInscripcionesController> logger,
                                    IColmagInscripcionesManager ColmagInscripcionesManager)
        {
            this.logger = logger;
            this.ColmagInscripcionesManager = ColmagInscripcionesManager;
        }

        //[HttpGet]
        [EnableQuery]
        public IEnumerable<ColmagInscripciones> Get()
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return this.ColmagInscripcionesManager.GetAll();
        }

        [HttpGet("(COLMAGInscripcionId={keyColmagInscripcionId})")]
        [EnableQuery]
        public IActionResult Get([FromODataUri] int keyColmagInscripcionId)
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            var row = this.ColmagInscripcionesManager.GetById(keyColmagInscripcionId);

            if (row == null)
            {
                return BadRequest($"Error buscando, Fila no existe.");
            }

            return Ok(row);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ColmagInscripciones row)
        {
        
            try
            {
                var orgrow = this.ColmagInscripcionesManager.Add(row);
                if (orgrow == null)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    return BadRequest($"Llave primaria duplicada ({row.ColmagInscripcionId})");
                }
                else
                {
                    this.ColmagInscripcionesManager.SaveChanges();
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
        public IActionResult Patch([FromODataUri] int keyColmagInscripcionId, Delta<ColmagInscripciones> changes)
        {
            try
            {
                var row = this.ColmagInscripcionesManager.Update(keyColmagInscripcionId, changes);
                if (row == null)
                {
                    return BadRequest($"Error actualizando, Fila no existe.");
                }
                else
                {
                    this.ColmagInscripcionesManager.SaveChanges();
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
        public IActionResult Delete([FromODataUri] int keyColmagInscripcionId)
        {
            try
            {
                var row = this.ColmagInscripcionesManager.Delete(keyColmagInscripcionId);
                if (row == null)
                {
                    return BadRequest($"Error eliminando, Fila no existe.");
                }
                else
                {
                    this.ColmagInscripcionesManager.SaveChanges();
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
