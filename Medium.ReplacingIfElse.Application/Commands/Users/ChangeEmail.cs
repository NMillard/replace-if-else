using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.interfaces.repositories;
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

    public interface IEmailChangeInput {

        /// <summary>
        /// Email to be changed. This is also used to look up the user.
        /// </summary>
        public string OldEmail { get; } // <- no setter!
        
        /// <summary>
        /// The new email
        /// </summary>
        public string NewEmail { get; } // <- no setter!
    }
}