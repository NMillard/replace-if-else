using System.ComponentModel.DataAnnotations;
using Medium.ReplacingIfElse.Application.Commands.Users;

namespace Medium.ReplacingIfElse.WebClient.RequestModels {
    /// <summary>
    /// Model the incoming request used to update only a user's username.
    /// </summary>
    public class ChangeUsernameInput : IChangeUsernameInput {
        [EmailAddress]
        public string Email { get; set; }
        public string NewUsername { get; set; }
    }
}