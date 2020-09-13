using System;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers.Commands;
using Medium.ReplacingIfElse.Application.Commands.Inputs;
using Medium.ReplacingIfElse.Application.Interfaces.Messaging;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class NotifyMarketingOnEmailUpdateHandler : ICommandHandlerAsync<ChangeEmailCommand> {
        private readonly IMessaging messageBus;

        public NotifyMarketingOnEmailUpdateHandler(IMessaging messageBus) {
            this.messageBus = messageBus;
        }
        
        // Notice this is the exact same code as in the ChangeEmail.cs
        public async Task HandleAsync(ChangeEmailCommand command) {
            // Let's use some guard clauses
            if (string.IsNullOrEmpty(command.OldEmail)) return;
            if (string.IsNullOrEmpty(command.NewEmail)) return;

            // Generate new security stamp
            
            // Notify marketing
            var message = new {
                command.OldEmail,
                command.NewEmail,
                UpdateTime = DateTime.UtcNow,
            };
            await messageBus.SendAsync(message, "marketing");
        }
    }
}