using Medium.ReplacingIfElse.Application.DependencyInjection;
using Medium.ReplacingIfElse.DataLayer.DependencyInjection;
using Medium.ReplacingIfElse.Messaging.DependencyInjection;
using Medium.ReplacingIfElse.WebClient.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Medium.ReplacingIfElse.WebClient {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddStackExchangeRedisCache(setup => {
                setup.Configuration = Configuration.GetConnectionString("cache");
            });

            // Extension methods from DataLayer project
            services.AddApplicationPersistence(Configuration.GetConnectionString("default"))
                .AddRepositories();

            services.AddMessaging(Configuration["queue"]);

            // Extension methods from Application project
            services.AddCommands()
                .AddQueries()
                .AddServices()
                .AddCommandDispatcher();

            // Just a simple background service consuming marketing messages on the queue
            services.AddHostedService(provider => {
                var logger = provider.GetRequiredService<ILogger<MarketingSimulationBackgroundService>>();
                return new MarketingSimulationBackgroundService(
                    Configuration["queue"],
                    logger
                );
            });
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