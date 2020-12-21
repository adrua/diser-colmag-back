using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inscripciones_Backend.Models
{
    public class ValidTokenHandler : AuthorizationHandler<ValidTokenHandler>, IAuthorizationRequirement
    {
        private static readonly HttpClient client = new HttpClient();
        public IConfiguration config { get; }
        public ValidTokenHandler (IConfiguration config)
        {
            this.config = config;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenHandler requirement)
        {
            try
            {
                var authFilterCtx = (Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext)context.Resource;
                string authHeader = authFilterCtx.HttpContext.Request.Headers["Authorization"];
                var token = authHeader.Replace("Bearer ", string.Empty);
                if (!await AuthorizeAsync(token))
                {
                    context.Fail();
                    return;
                }

                context.Succeed(requirement);
            }
            catch (Exception e)
            {
                context.Fail();
            }
        }
        
        private async Task<bool> AuthorizeAsync(string token)
        {
            dynamic result = new { Success = true };

            return result.Success;
        }
    }

}
