using System;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.interfaces.repositories;
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

        public async Task UpdateUserAsync(UpdateReason updateReason, IUpdateUser userInfo) {

            User? user = await repository.FindByEmailAsync(userInfo.OriginalEmail);
            if (user is null) return;
            
            // This kinda code is a trip down memory lane.
            // Filled with nightmares and horrors...
            if (updateReason == UpdateReason.EmailChanged) {
                
            } else if (updateReason == UpdateReason.UsernameChanged) {
                
            } else {
                throw new ArgumentException();
            }
        }
    }
    
    public interface IUpdateUser {
        string OriginalEmail { get; }
        string Email { get; }
        string Username { get; }
    }
    
    public enum UpdateReason {
        EmailChanged,
        UsernameChanged,
    }
}