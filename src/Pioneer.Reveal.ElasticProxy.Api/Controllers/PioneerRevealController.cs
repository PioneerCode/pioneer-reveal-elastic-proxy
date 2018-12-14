using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Reveal.ElasticProxy.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class PioneerRevealController : ControllerBase
    {
        private readonly Repository.IElasticsearchRepository _elasticsearchRepository;

        public PioneerRevealController(Repository.IElasticsearchRepository elasticsearchRepository)
        {
            _elasticsearchRepository = elasticsearchRepository;
        }

        [HttpGet(Repository.ProxyRoutes.GetIndices)]
        public async Task<ActionResult<Entites.Index[]>> GetIndicesAsync()
        {
            return await _elasticsearchRepository.GetIndicesAsync();
        }

        [HttpPost(Repository.ProxyRoutes.GetLogs)]
        public async Task<ActionResult<dynamic>> GetIndicesAsync(string indices, dynamic request)
        {
            return await _elasticsearchRepository.GetLogsAsync(indices, request);
        }
    }
}