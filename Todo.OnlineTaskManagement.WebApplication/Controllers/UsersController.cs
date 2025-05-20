using Todo.OnlineTaskManagement.ApplicationCore.Entities;
using Todo.OnlineTaskManagement.Shared.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.WebApplication.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UsersController(UserManager<ApplicationUser> userManager) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new ApplicationUserForView
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                Username = user.UserName
            });
        }
    }
}
