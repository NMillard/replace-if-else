using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Application.DependencyInjection {
    public static class ServiceInjector {

        /// <summary>
        /// Dynamically register all types of <see cref="ICommandHandlerAsync{TCommand}"/> from this assembly.<br />
        /// </summary>
        public static IServiceCollection AddCommandDispatcher(this IServiceCollection services) {
            Type[] handlers = Assembly.GetExecutingAssembly().GetTypes()
                // Find all types where
                .Where(t => t.IsClass && // the type is a class
                            t.GetInterfaces() // and the type implements the generic type definition ICommandHandlerAsync
                                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>)))
                .ToArray();
            
            // Register the interface with the concrete handler.
            // Note: the current limitation is, every handler must be in a separate class.
            // This won't work with a single class implementing the ICommandHandlerAsync<> multiple times.
            foreach (Type concreteHandler in handlers) {
                Type? handlerInterface = concreteHandler
                    .GetInterfaces()
                    .First(i => i.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>));
                
                services.AddScoped(handlerInterface, concreteHandler);
            }

            // this factory method is called every time a CommandDispatcher is required
            services.AddScoped<CommandDispatcher>(provider => {
                var handlerTypes = new Dictionary<Type, IEnumerable<object>>();

                ServiceDescriptor[] descriptors = services
                    .Where(t => t.ServiceType.IsGenericType &&
                                t.ServiceType.GetGenericTypeDefinition() == typeof(ICommandHandlerAsync<>))
                    .ToArray();

                foreach (var serviceDescriptor in descriptors) {
                    handlerTypes[serviceDescriptor.ServiceType] = provider.GetServices(serviceDescriptor.ServiceType);
                }
                
                return new CommandDispatcher(handlerTypes);
            });
            
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