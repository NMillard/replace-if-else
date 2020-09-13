namespace Medium.ReplacingIfElse.Application.CommandHandlers.Commands {
    public class ChangeUsernameCommand {
        public ChangeUsernameCommand(string email, string newUsername) {
            Email = email;
            NewUsername = newUsername;
        }
        
        public string Email { get; }
        public string NewUsername { get; }
    }
}