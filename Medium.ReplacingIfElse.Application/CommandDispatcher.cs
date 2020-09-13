using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers;
using Microsoft.Extensions.Logging;

namespace Medium.ReplacingIfElse.Application {
    public class CommandDispatcher {
        private readonly Dictionary<Type, IEnumerable<object>> handlers;
        private readonly ILogger<CommandDispatcher>? logger;

        /// <param name="handlers">
        ///     Provide a dictionary where the key is the type of command handler
        ///     and value is a list of all handlers for that specific command.
        /// </param>
        /// <param name="logger">The logger is optional. Null is safely passed in.</param>
        public CommandDispatcher(Dictionary<Type, IEnumerable<object>> handlers, ILogger<CommandDispatcher>? logger = null) {
            this.handlers = handlers;
            this.logger = logger;
        }
        
        /// <summary>
        ///     Pass the command object to any handler that knows how to deal with it. <br />
        ///     Only the concrete class of the <paramref name="command"/> is used to discover handlers.
        /// </summary>
        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class {
            logger?.LogInformation($"Receiving command {command.GetType()}");
            
            // This basically places the Type of 'command' inside the ICommandHandlerAsync's generic brackets
            Type handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            handlers.TryGetValue(handlerType, out IEnumerable<object>? concreteHandlers);
            if (concreteHandlers is null) throw new ArgumentException($"No handler registered for {command.GetType()}");

            foreach (object concreteHandler in concreteHandlers) {
                var handler = concreteHandler as ICommandHandlerAsync<TCommand>;
                if (handler is null) continue;
                
                logger?.LogInformation($"Invoking handler {handler.GetType()}");
                await handler.HandleAsync(command);
            }
        }
    }
}