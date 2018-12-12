using Microsoft.Extensions.DependencyInjection;

namespace Pioneer.Reveal.ElasticProxy.Api.WindowsAuthentication.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterPioneerReveal(this IServiceCollection services, string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                services.AddTransient<IProxy>(s => new Proxy(url));
            }
            else
            {
                services.AddTransient<IProxy>(s => new Proxy());
            }
        }
    }
}
