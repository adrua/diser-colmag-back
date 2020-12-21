using Inscripciones_Backend.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inscripciones_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            var row = new { token = "ABCXYZ123456" };
            return Ok(row);
        }
    }
}
