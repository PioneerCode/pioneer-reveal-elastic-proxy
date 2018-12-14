using Microsoft.Extensions.DependencyInjection;
using Pioneer.Reveal.ElasticProxy.Api.Repository;
using Pioneer.Reveal.ElasticProxy.Api.Services;

namespace Pioneer.Reveal.ElasticProxy.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterPioneerReveal(this IServiceCollection services, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                services.AddTransient<IElasticsearchRepository>(s => new ElasticsearchRepository(url));
            }
            else
            {
                services.AddTransient<IElasticsearchRepository>(s => new ElasticsearchRepository());
            }

            services.AddScoped<IUserService, UserService>();
        }
    }
}
