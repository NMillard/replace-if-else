using System;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers.Commands;
using Medium.ReplacingIfElse.Application.Interfaces.Messaging;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class NotifyMarketingOnEmailUpdateHandler : ICommandHandlerAsync<ChangeEmailCommand> {
        private readonly IMessaging messageBus;

        public NotifyMarketingOnEmailUpdateHandler(IMessaging messageBus) {
            this.messageBus = messageBus;
        }
        
        // Notice this is the exact same code as in the ChangeEmail.cs
        public async Task HandleAsync(ChangeEmailCommand command) {
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