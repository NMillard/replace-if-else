using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Application {
    public class CommandDispatcher {
        private readonly IServiceCollection services;

        public CommandDispatcher(IServiceCollection services) {
            this.services = services;
        }
        
        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class {
            // This basically places the Type of 'command' inside the ICommandHandlerAsync's generic brackets
            Type handler = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            IEnumerable<object>? concreteHandlers = services.BuildServiceProvider().GetServices(handler);

            foreach (object concreteHandler in concreteHandlers) {
                await ((ICommandHandlerAsync<TCommand>) concreteHandler).HandleAsync(command);
            }
        }
    }
}