using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.ReplacingIfElse.Application.Commands.Users;
using Medium.ReplacingIfElse.Application.Queries.Users;
using Medium.ReplacingIfElse.Domain;
using Medium.ReplacingIfElse.WebClient.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Medium.ReplacingIfElse.WebClient.Controllers {
    
    /// <summary>
    /// User controller that takes every operation as a separate dependency. <br />
    /// This is essentially a poor man's CQS. Which is a great pragmatic approach. <br />
    ///
    /// Easy to understand, easy to implement, easy to extend.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase {
        private readonly GetUsers getUsers;
        private readonly ChangeEmail changeEmail;
        private readonly ChangeUsername changeUsername;
        private readonly IDistributedCache cache;

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
            ChangeUsername changeUsername,
            IDistributedCache cache
            ) {
            this.getUsers = getUsers;
            this.changeEmail = changeEmail;
            this.changeUsername = changeUsername;
            this.cache = cache;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetUsers() {
            string s = await cache.GetStringAsync("users");
            
            if (s is null) {
                var list = new List<User>();
                
                IAsyncEnumerable<User> users = await getUsers.ExecuteAsync();
                await foreach (User user in users) list.Add(user);

                await cache.SetStringAsync("users", JsonConvert.SerializeObject(list), new DistributedCacheEntryOptions {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),
                });
                return Ok(users);
            }
            
            var e = JsonConvert.DeserializeObject<List<User>>(s);
            
            return Ok(e);
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
    }
}