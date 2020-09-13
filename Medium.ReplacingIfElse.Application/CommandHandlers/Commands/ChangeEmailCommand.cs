namespace Medium.ReplacingIfElse.Application.CommandHandlers.Commands {
    public class ChangeEmailCommand {
        public ChangeEmailCommand(string oldEmail, string newEmail) {
            OldEmail = oldEmail;
            NewEmail = newEmail;
        }
        
        public string OldEmail { get; }
        public string NewEmail { get; }
    }
}