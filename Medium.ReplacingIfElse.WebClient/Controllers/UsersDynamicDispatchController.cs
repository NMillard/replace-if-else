using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Medium.ReplacingIfElse.Application.CommandHandlers.Users;
using Microsoft.AspNetCore.Mvc;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    [ApiController]
    [Route("api/users")]
    public class UsersDynamicDispatchController : ControllerBase {
        private readonly CommandDispatcher dispatcher;

        public UsersDynamicDispatchController(CommandDispatcher dispatcher) {
            this.dispatcher = dispatcher;
        }
        
        [HttpPost("dispatch/UpdateEmail")]
        public async Task<IActionResult> UpdateEmailWithDispatcher(UsersController.ChangeEmailInput input) {
            await dispatcher.DispatchAsync(new UpdateEmailCommand(input.OldEmail, input.NewEmail));
            
            return Ok();
        }
    }
}