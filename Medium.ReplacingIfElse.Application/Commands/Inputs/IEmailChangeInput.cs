namespace Medium.ReplacingIfElse.Application.Commands.Inputs {
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