using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Reveal.ElasticProxy.Api.WindowsAuthentication.Controllers
{
    public class PioneerRevealController : ControllerBase
    {
        private readonly IProxy _proxy;

        public PioneerRevealController(IProxy proxy)
        {
            _proxy = proxy;
        }

        [HttpGet(ProxyRoutes.GetIndices)]
        public async Task<ActionResult<Index[]>> GetIndicesAsync()
        {
            return await _proxy.GetIndicesAsync();
        }

        [HttpPost(ProxyRoutes.GetLogs)]
        public async Task<ActionResult<dynamic>> GetIndicesAsync(string indices, dynamic request)
        {
            return await _proxy.GetLogsAsync(indices, request);
        }
    }
}