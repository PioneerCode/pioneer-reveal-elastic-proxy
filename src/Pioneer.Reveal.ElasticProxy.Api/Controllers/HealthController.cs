using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Reveal.ElasticProxy.Api.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Get API version
        /// </summary>
        /// <response code="200">API is alive</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Route("api/version")]
        public ActionResult<string> GetVersionAsync()
        {
            return Ok(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
        }
    }
}