using Medium.ReplacingIfElse.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Messaging.DependencyInjection {
    public static class ServiceInjector {
        public static IServiceCollection AddMessaging(this IServiceCollection services, string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) {
                services.AddScoped<IMessaging, NullMessaging>();
                return services;
            }

            services.AddScoped<IMessaging>(provider => new AzureMessagingQueue(connectionString));
            
            return services;
        }
    }
}