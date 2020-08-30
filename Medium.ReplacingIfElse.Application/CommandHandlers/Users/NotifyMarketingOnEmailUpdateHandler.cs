using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.interfaces.repositories;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class NotifyMarketingOnEmailUpdateHandler : ICommandHandlerAsync<UpdateEmailCommand> {
        private readonly IUserRepository repository;

        public NotifyMarketingOnEmailUpdateHandler(IUserRepository repository) {
            this.repository = repository;
        }
        
        // Notice this is the exact same code as in the ChangeEmail.cs
        public Task HandleAsync(UpdateEmailCommand command) {
            // Let's use some guard clauses
            if (string.IsNullOrEmpty(command.OldEmail)) return Task.CompletedTask;
            if (string.IsNullOrEmpty(command.NewEmail)) return Task.CompletedTask;

            // Generate new security stamp
            // Notify marketing
            
            return Task.CompletedTask;
        }
    }
}