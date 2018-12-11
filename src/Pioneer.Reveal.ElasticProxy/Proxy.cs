using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pioneer.Reveal.Elastic
{
    public interface IProxy
    {
        Task<Index[]> GetIndicesAsync();
        Task<dynamic> GetLogsAsync(string index, dynamic request);
    }

    /// <summary>
    /// Serves as a proxy to the ElasticSearch service.
    ///
    /// This can be used in a .net Web Api and sit behind a controller that is
    /// controlled by a secure endpoint.
    ///
    /// ElasticSearch itself is not secured, so we only expose it on an internal network.
    /// </summary>
    public class Proxy : IProxy
    {
        private readonly string _url;

        public Proxy()
        {
            _url = "http://localhost:9200";
        }

        public Proxy(string url)
        {
            _url = url;
        }

        /// <summary>
        /// Get all available indices: /_cat/indices?format=json
        /// </summary>
        /// <returns>Elastic Index result body</returns>
        public async Task<Index[]> GetIndicesAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_url}/{ProxyRoutes.GetIndices}?format=json");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Index[]>(responseBody);
            }
        }

        /// <summary>
        /// Perform a search against indices : /_search?format=json
        /// </summary>
        /// <param name="index">Comma separated list of indices to include in search</param>
        /// <param name="request">Elastic Search result body</param>
        /// <returns>Elastic search request body</returns>
        public async Task<dynamic> GetLogsAsync(string index, dynamic request)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_url}/{index}/_search?format=json", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(responseBody);
            }
        }
    }
}
