using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Application.DependencyInjection {
    public static class ServiceInjector {

        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddScoped<UserService>();
            
            return services;
        }
        
        public static IServiceCollection AddCommands(this IServiceCollection services) {
            services.AddScoped<ChangeEmail>();
            services.AddScoped<ChangeUsername>();
            
            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services) {
            services.AddScoped<GetUsers>();
            
            return services;
        }
    }
}