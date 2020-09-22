using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.CommandHandlers.Commands;
using Medium.ReplacingIfElse.Application.Interfaces.Repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class UpdateEmailHandler : ICommandHandlerAsync<ChangeEmailCommand> {
        private readonly IUserRepository repository;

        public UpdateEmailHandler(IUserRepository repository) {
            this.repository = repository;
        }
        
        // Notice this is the exact same code as in the ChangeEmail.cs
        // Except the notify marketing part is no longer needed.
        public async Task HandleAsync(ChangeEmailCommand command) {
            User? user = await repository.FindByEmailAsync(command.OldEmail);
            if (user is null) return;
            
            user.ChangeEmail(command.NewEmail);
            
            // Generate new security stamp

            await repository.UpdateAsync(user);
        }
    }
}