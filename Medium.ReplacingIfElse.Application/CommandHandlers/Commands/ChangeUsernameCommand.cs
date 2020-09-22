using System;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Commands {
    public class ChangeUsernameCommand {
        public ChangeUsernameCommand(string email, string newUsername) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newUsername))
                throw new ArgumentException();
            
            Email = email;
            NewUsername = newUsername;
        }
        
        public string Email { get; }
        public string NewUsername { get; }
    }
}