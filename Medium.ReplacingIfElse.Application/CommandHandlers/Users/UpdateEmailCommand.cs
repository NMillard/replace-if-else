namespace Medium.ReplacingIfElse.Application.CommandHandlers.Users {
    public class UpdateEmailCommand {
        public UpdateEmailCommand(string oldEmail, string newEmail) {
            OldEmail = oldEmail;
            NewEmail = newEmail;
        }
        
        public string OldEmail { get; }
        public string NewEmail { get; }
    }
}