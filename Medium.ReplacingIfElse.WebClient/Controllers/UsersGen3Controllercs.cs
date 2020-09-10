using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Microsoft.AspNetCore.Mvc;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    /// <summary>
    /// A dynamic controller that sends commands to a dispatcher. <br />
    ///
    /// It's incredibly easy to test, as we only need a single dependency on
    /// <see cref="CommandDispatcher"/>. <br />
    ///
    /// The trade-off is, it's slightly more difficult to reason about than the
    /// <see cref="UsersController"/>, if you're not used to CQS. 
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersDynamicDispatchController : ControllerBase {
        private readonly CommandDispatcher dispatcher;

        public UsersDynamicDispatchController(CommandDispatcher dispatcher) {
            this.dispatcher = dispatcher;
        }
        
        [HttpPost("dispatch/UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(UsersController.ChangeEmailInput input) {
            await dispatcher.DispatchAsync(new UpdateEmailCommand(input.OldEmail, input.NewEmail));
            
            return Ok();
        }
    }
}