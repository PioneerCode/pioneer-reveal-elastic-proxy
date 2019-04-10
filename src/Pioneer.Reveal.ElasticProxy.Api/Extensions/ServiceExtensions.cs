using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

        public static void AddJwtAuthntication(this IServiceCollection services, string jwtSecret)
        {
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
