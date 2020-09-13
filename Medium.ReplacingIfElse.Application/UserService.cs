using System;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Interfaces.Repositories;
using Medium.ReplacingIfElse.Domain;

namespace Medium.ReplacingIfElse.Application {
    
    /// <summary>
    /// This is just an old-school Service class.
    /// It's meant to represent what we need to move away from.
    /// </summary>
    public class UserService {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository) {
            this.repository = repository;
        }

        // This kinda code is a trip down memory lane.
        // Filled with nightmares and horrors...
        public async Task UpdateUserAsync(UpdateReason updateReason, IUpdateUser userInfo) {

            User? user = await repository.FindByEmailAsync(userInfo.OriginalEmail);
            if (user is null) return;
            
            if (updateReason == UpdateReason.EmailChanged) {
                user.ChangeEmail(userInfo.UpdatedEmail);
                // Generate new security stamp
                // Notify marketing
            } else if (updateReason == UpdateReason.UsernameChanged) {
                user.ChangeUsername(userInfo.UpdatedUsername);
                // Update profile slug
                // Notify user's followers about the username change
            } else {
                throw new ArgumentException();
            }
            
            await repository.UpdateAsync(user);
        }
    }
    
    public interface IUpdateUser {
        string OriginalEmail { get; }
        string UpdatedEmail { get; }
        string UpdatedUsername { get; }
    }
    
    public enum UpdateReason {
        EmailChanged,
        UsernameChanged,
    }
}