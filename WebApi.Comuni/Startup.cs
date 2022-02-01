using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Comuni.Models.Services.Application;
using WebApi.Comuni.Models.Services.Infrastructure;

namespace WebApi.Comuni
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Comuni", Version = "v1" });
            });

            services.AddTransient<ILocationService, EfCoreLocationService>();

            services.AddDbContextPool<ApplicationDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString, options =>
                {
                    // Abilitazione del connection resiliency (Non � supportato dal provider di Sqlite perch� non � soggetto a errori transienti)
                    // Per informazioni consultare la pagina: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                    // options.EnableRetryOnFailure(3);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Comuni v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
