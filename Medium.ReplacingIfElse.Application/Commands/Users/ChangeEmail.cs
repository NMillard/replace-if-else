using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Commands.Inputs;
using Medium.ReplacingIfElse.Application.Interfaces.Repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.Commands.Users {
    public class ChangeEmail : ICommandAsync<IEmailChangeInput> {
        private readonly IUserRepository repository;

        public ChangeEmail(IUserRepository repository) {
            this.repository = repository;
        }
        
        public async Task ExecuteAsync(IEmailChangeInput input) {
            // Let's use some guard clauses
            if (string.IsNullOrEmpty(input.OldEmail)) return;
            if (string.IsNullOrEmpty(input.NewEmail)) return;
            
            User? user = await repository.FindByEmailAsync(input.OldEmail);
            if (user is null) return;
            
            user.ChangeEmail(input.NewEmail);
            
            // Generate new security stamp
            // Notify marketing

            await repository.UpdateAsync(user);
        }
    }
}