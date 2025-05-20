using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.OnlineTaskManagement.Application.Services;
using Todo.OnlineTaskManagement.Shared.Requests;

namespace Todo.OnlineTaskManagement.WebApplication.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AuthController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.Register(model);

            if (result.Equals("-1"))
            {
                return BadRequest("User Not Created :(");
            }

            return Ok(result);
        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.Login(model);

            if (result.Token.Contains("User name does not exist! :("))
            {
                return NotFound(model.Username + " does not exist");
            }

            return Ok(result);
        }
    }
}