using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pioneer.Reveal.ElasticProxy.Api.Services;

namespace Pioneer.Reveal.ElasticProxy.Api.Extensions
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

            services.AddScoped<IUserService, UserService>();
        }
    }
}
