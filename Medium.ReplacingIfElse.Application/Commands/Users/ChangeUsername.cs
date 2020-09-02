using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Interfaces.repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application.Commands.Users {
    public class ChangeUsername : ICommandAsync<IChangeUsernameInput> {
        private readonly IUserRepository repository;

        public ChangeUsername(IUserRepository repository) {
            this.repository = repository;
        }

        public async Task ExecuteAsync(IChangeUsernameInput input) {
            if (string.IsNullOrEmpty(input.Email)) return;
            
            User? user = await repository.FindByEmailAsync(input.Email);
            if (user is null) return;
            
            user.ChangeUsername(input.NewUsername);

            // Update profile slug
            // Notify user's followers about the username change

            await repository.UpdateAsync(user);
        }
    }

    public interface IChangeUsernameInput {
        string Email { get; }
        string NewUsername { get; }
    }
}