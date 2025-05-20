using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.OnlineTaskManagement.ApplicationCore.Entities;
using Todo.OnlineTaskManagement.Shared.Requests;

namespace Todo.OnlineTaskManagement.Application.Services
{
    public class AccountService(
       UserManager<ApplicationUser> userManager,
       SignInManager<ApplicationUser> signInManager,
       IConfiguration configuration)
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;
        private readonly SignInManager<ApplicationUser> signInManager = signInManager;
        private readonly IConfiguration configuration = configuration;

        public async Task<string> Register(RegistrationDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                PhoneNumber = model.MobileNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var createUser = await this.userManager.CreateAsync(user, model.Password);

            if (createUser.Succeeded)
            {
                await signInManager.SignInAsync(user, false);

                return "User Registered :)";
            }

            return "-1";
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var appUser = this.userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == model.Username.ToLower());

                var token = GenerateJwtToken(appUser);

                return new UserLoginResponse() { Token = token.ToString() };
            }

            return new UserLoginResponse() { Token = "User name does not exist! :(" };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtIssuerOptions:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtIssuerOptions:Expires"]));
            var iss = configuration["JwtIssuerOptions:Issuer"];
            var aud = configuration["JwtIssuerOptions:Audience"];
            var token = new JwtSecurityToken(
                iss,
                aud,
                userClaims,
                expires: expires,
                signingCredentials: creds
            );

            string result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }
    }
}
