using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Medium.ReplacingIfElse.Application {
    public class CommandDispatcher {
        private readonly IServiceProvider provider;

        public CommandDispatcher(IServiceProvider provider) {
            this.provider = provider;
        }
        
        /// <summary>
        /// Pass the command object to any handler that knows how to deal with it.
        /// </summary>
        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class {
            // This basically places the Type of 'command' inside the ICommandHandlerAsync's generic brackets
            Type handler = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            // Some may perceive the service-locator pattern this to be an anti-pattern.
            // But it's pretty damn handy, when you want to create applications that
            // follow the Open/Closed principle.
            IEnumerable<object> concreteHandlers = provider.GetServices(handler);

            foreach (object concreteHandler in concreteHandlers) {
                await ((ICommandHandlerAsync<TCommand>) concreteHandler).HandleAsync(command);
            }
        }
    }
}