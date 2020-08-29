using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Medium.ReplacingIfElse.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly GetUsers getUsers;
        private readonly ChangeEmail changeEmail;
        private readonly ChangeUsername changeUsername;
        private readonly UserService userService;
        private readonly CommandDispatcher dispatcher;

        /* Take commands as dependencies.
         * This may get awkward once a constructor
         * takes lots of commands...
         *
         * Good thing is, we get rid of Service classes,
         * which are basically logic dumpsters.
         *
         * Let the controller actions follow an Accept-Delegate-Reply pattern.
         */
        public UsersController(
            GetUsers getUsers,
            ChangeEmail changeEmail,
            ChangeUsername changeUsername,
            UserService userService, // <- Old-school service class.
            CommandDispatcher dispatcher // <- dynamic command dispatcher
            ) {
            this.getUsers = getUsers;
            this.changeEmail = changeEmail;
            this.changeUsername = changeUsername;
            this.userService = userService;
            this.dispatcher = dispatcher;
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
        /// Old-School, CRUD inspired, useless endpoint that'll
        /// bloat your code and ruin your sleep.
        ///
        /// Don't do this.
        /// </summary>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateUser input) {
            await userService.UpdateUserAsync(input.UpdateReason, input);
            
            return Ok();
        }
        
        [HttpPost("UpdateEmailWithDispatcher")]
        public async Task<IActionResult> UpdateEmailWithDispatcher(ChangeEmailInput input) {
            await dispatcher.DispatchAsync(new UpdateEmailCommand(input.OldEmail, input.NewEmail));
            
            return Ok();
        }

        public class UpdateUser : IUpdateUser {
            public UpdateReason UpdateReason { get; set; }
            [EmailAddress]
            public string OriginalEmail { get; set; }
            [EmailAddress]
            public string UpdatedEmail { get; set; }
            public string UpdatedUsername { get; set; }
        }
        
        public class ChangeUsernameInput : IChangeUsernameInput {
            [EmailAddress]
            public string Email { get; set; }
            public string NewUsername { get; set; }
        }
        
        public class ChangeEmailInput : IEmailChangeInput {

            [EmailAddress]
            public string OldEmail { get; set; }
            
            [EmailAddress]
            public string NewEmail { get; set; }

        }
    }
}