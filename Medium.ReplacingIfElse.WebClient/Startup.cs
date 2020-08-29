using Medium.ReplacingIfElse.Application.DependencyInjection;
using Medium.ReplacingIfElse.DataLayer.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Medium.ReplacingIfElse.WebClient {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            // Extension methods from DataLayer project
            services.AddApplicationPersistence(Configuration.GetConnectionString("default"))
                .AddRepositories();
            
            // Extension methods from Application project
            services.AddCommands()
                .AddQueries()
                .AddServices()
                .AddCommandDispatcher();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}