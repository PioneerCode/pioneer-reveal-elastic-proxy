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

        public static void AddPioneerRevealAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["PioneerReveal:SiteUrl"],
                        ValidAudience = config["PioneerReveal:SiteUrl"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["PioneerReveal:JwtSecret"]))
                    };
                });
        }
    }
}
