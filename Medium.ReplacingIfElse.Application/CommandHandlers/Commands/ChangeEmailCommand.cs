using System;

namespace Medium.ReplacingIfElse.Application.CommandHandlers.Commands {
    public class ChangeEmailCommand {
        public ChangeEmailCommand(string oldEmail, string newEmail) {
            if (string.IsNullOrEmpty(oldEmail) || string.IsNullOrEmpty(newEmail))
                throw new ArgumentException();
            
            OldEmail = oldEmail;
            NewEmail = newEmail;
        }
        
        public string OldEmail { get; }
        public string NewEmail { get; }
    }
}