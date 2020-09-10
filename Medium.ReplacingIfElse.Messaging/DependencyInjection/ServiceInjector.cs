using System;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Medium.ReplacingIfElse.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Messaging.DependencyInjection {
    public static class ServiceInjector {
        public static IServiceCollection AddMessaging(this IServiceCollection services, string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException(nameof(connectionString));
            
            services.AddScoped<IMessaging>(provider => new Messaging(connectionString));
            
            return services;
        }
    }
}