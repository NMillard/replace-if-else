using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers;

namespace Medium.ReplacingIfElse.Application {
    public class CommandDispatcher {
        private readonly Dictionary<Type, IEnumerable<object>> handlers;

        /// <param name="handlers">Provide a dictionary where the key is the type of command handler
        /// and value is a list of all handlers for that specific command.</param>
        public CommandDispatcher(Dictionary<Type, IEnumerable<object>> handlers) {
            this.handlers = handlers;
        }
        
        /// <summary>
        /// Pass the command object to any handler that knows how to deal with it.
        /// </summary>
        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class {
            // This basically places the Type of 'command' inside the ICommandHandlerAsync's generic brackets
            Type handlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(command.GetType());

            handlers.TryGetValue(handlerType, out IEnumerable<object>? concreteHandlers);
            if (concreteHandlers is null) throw new ArgumentException($"No handler registered for {command.GetType()}");

            foreach (object concreteHandler in concreteHandlers) {
                var handler = concreteHandler as ICommandHandlerAsync<TCommand>;
                if (handler is null) continue;
                
                await handler.HandleAsync(command);
            }
        }
    }
}