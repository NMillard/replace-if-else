using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application;
using Microsoft.AspNetCore.Mvc;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    /// <summary>
    /// This is the traditional kind of controller. <br />
    /// It takes a dependency on a logic dumpster, disguised as a "service". <br />
    ///
    /// This is difficult to reason about and maintain.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersServiceController : ControllerBase {
        private readonly UserService service;

        public UsersServiceController(UserService service) {
            this.service = service;
        }
        
        /// <summary>
        /// Old-School, CRUD inspired, useless endpoint that'll
        /// bloat your code and ruin your sleep.
        ///
        /// Don't do this.
        /// </summary>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateUser input) {
            await service.UpdateUserAsync(input.UpdateReason, input);
            
            return Ok();
        }
        
        /// <summary>
        /// Model the incoming request used to update a user.
        /// </summary>
        public class UpdateUser : IUpdateUser {
            public UpdateReason UpdateReason { get; set; }
            [EmailAddress]
            public string OriginalEmail { get; set; }
            [EmailAddress]
            public string UpdatedEmail { get; set; }
            public string UpdatedUsername { get; set; }
        }
    }
}