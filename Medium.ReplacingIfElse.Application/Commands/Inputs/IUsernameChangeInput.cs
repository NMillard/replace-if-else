namespace Medium.ReplacingIfElse.Application.Commands.Inputs {
    public interface IUsernameChangeInput {
        /// <summary>
        /// Email of the user that needs its username changed.
        /// </summary>
        public string Email { get; }
        
        public string NewUsername { get; }
    }
}