using System.ComponentModel.DataAnnotations;
using Medium.ReplacingIfElse.Application.Commands.Inputs;
using Medium.ReplacingIfElse.Application.Commands.Users;

namespace Medium.ReplacingIfElse.WebClient.RequestModels {
    /// <summary>
    /// Model the incoming request used to update only a user's email.
    /// </summary>
    public class ChangeEmailInput : IEmailChangeInput {

        [EmailAddress]
        public string OldEmail { get; set; }
            
        [EmailAddress]
        public string NewEmail { get; set; }

    }
}