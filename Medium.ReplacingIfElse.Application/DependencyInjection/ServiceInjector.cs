using System;
using System.Linq;
using System.Reflection;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Application.DependencyInjection {
    public static class ServiceInjector {

        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services) {
            Type[] handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>)))
                .ToArray();

            foreach (Type handler in handlers) {
                Type? handlerType = handler
                    .GetInterfaces()
                    .First(i => i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>));
                services.AddScoped(handlerType, handler);
            }

            services.AddScoped<CommandDispatcher>(_ => new CommandDispatcher(services));
            
            return services;
        }
        
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