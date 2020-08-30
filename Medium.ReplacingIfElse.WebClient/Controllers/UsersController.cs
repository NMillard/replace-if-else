using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Medium.ReplacingIfElse.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    /// <summary>
    /// User controller that takes every operation as a separate dependency. <br />
    /// This is essentially a poor man's CQS. <br />
    ///
    /// Easy to understand, easy to implement, easy to extend.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase {
        private readonly GetUsers getUsers;
        private readonly ChangeEmail changeEmail;
        private readonly ChangeUsername changeUsername;

        /* Take commands as dependencies.
         * This may get awkward once a constructor
         * takes lots of commands... and you may want to consider
         * using a command dispatcher.
         *
         * Good thing is, we get rid of Service classes,
         * which are basically logic dumpsters.
         *
         * Let the controller actions follow an Accept-Delegate-Reply pattern.
         */
        public UsersController(
            GetUsers getUsers,
            ChangeEmail changeEmail,
            ChangeUsername changeUsername
            ) {
            this.getUsers = getUsers;
            this.changeEmail = changeEmail;
            this.changeUsername = changeUsername;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetUsers() {
            IAsyncEnumerable<User> users = await getUsers.ExecuteAsync();

            return Ok(users);
        }

        [HttpPost("UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(ChangeEmailInput input) {
            // This says, "look, I don't care how it's done. Here's the input, go figure it out.
            // Let me know how it went...
            await changeEmail.ExecuteAsync(input);
            
            // Note:
            // We're obviously lacking some exception handling.
            // I'll let you deal with that. It's outside the scope of what
            // I'm trying to accomplish.
            
            return Ok();
        }

        [HttpPost("UpdateUsername")]
        public async Task<IActionResult> UpdateUsername(ChangeUsernameInput input) {
            await changeUsername.ExecuteAsync(input);
            return Ok();
        }

        
        /// <summary>
        /// Model the incoming request used to update only a user's username.
        /// </summary>
        public class ChangeUsernameInput : IChangeUsernameInput {
            [EmailAddress]
            public string Email { get; set; }
            public string NewUsername { get; set; }
        }
        
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
}