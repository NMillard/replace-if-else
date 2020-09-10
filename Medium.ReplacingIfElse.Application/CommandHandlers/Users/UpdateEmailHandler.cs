using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Interfaces.repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class UpdateEmailHandler : ICommandHandlerAsync<UpdateEmailCommand> {
        private readonly IUserRepository repository;

        public UpdateEmailHandler(IUserRepository repository) {
            this.repository = repository;
        }
        
        // Notice this is the exact same code as in the ChangeEmail.cs
        public async Task HandleAsync(UpdateEmailCommand command) {
            // Let's use some guard clauses
            if (string.IsNullOrEmpty(command.OldEmail)) return;
            if (string.IsNullOrEmpty(command.NewEmail)) return;
            
            User? user = await repository.FindByEmailAsync(command.OldEmail);
            if (user is null) return;
            
            user.ChangeEmail(command.NewEmail);
            
            // Generate new security stamp
            // Notify marketing

            await repository.UpdateAsync(user);
        }
    }
}