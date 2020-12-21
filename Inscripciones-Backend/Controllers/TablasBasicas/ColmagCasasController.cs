//ColmagCasasController.cs
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

        //[HttpGet]
        [EnableQuery]
        public IEnumerable<ColmagCasas> Get()
        {
            logger.Log(LogLevel.Information, $"Inicio de consumo del API: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return this.ColmagCasasManager.GetAll();
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
        public IActionResult Post([FromBody] ColmagCasas row)
        {
        
            try
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
            catch(Exception)
            {
                var errors = String.Join("\n", ModelState.Root.Errors.Select((e) => e.Exception.Message));
                return BadRequest($"Código repetido o datos inválidos\n{errors}");
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromODataUri] int keyColmagCasaId, Delta<ColmagCasas> changes)
        {
            try
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
            catch(Exception)
            {
                var errors = String.Join("\n", ModelState.Root.Errors.Select((e) => e.Exception.Message));
                return BadRequest($"Código repetido o datos inválidos\n{errors}");
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int keyColmagCasaId)
        {
            try
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
            catch (Exception)
            {
                return BadRequest($"Error eliminando, el registro está en uso.");
            }
        }
    }
}
