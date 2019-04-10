using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pioneer.Logs.Tubs.AspNetCore;
using Pioneer.Reveal.ElasticProxy.Api.Entites;
using Pioneer.Reveal.ElasticProxy.Api.Extensions;

namespace Pioneer.Reveal.ElasticProxy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("PioneerRevealConfiguration");
            services.Configure<PioneerRevealConfiguraiton>(appSettingsSection);
            var config = appSettingsSection.Get<PioneerRevealConfiguraiton>();

            services.RegisterPioneerReveal(config.ElasticUrl);
            services.AddPioneerLogs(Configuration.GetSection("PioneerLogsConfiguration"));
            services.AddJwtAuthntication(config.JwtSecret);
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UsePioneerLogs();

#if DEBUG
            app.UseHttpsRedirection();
#endif

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            app.UseMvc();
        }
    }
}
