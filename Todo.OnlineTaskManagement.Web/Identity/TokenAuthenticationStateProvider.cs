using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Todo.OnlineTaskManagement.Shared.Responses;

namespace Todo.OnlineTaskManagement.Web.Identity
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public TokenAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("AuthToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            if (IsTokenExpired(token))
            {
                throw new UnauthorizedAccessException();
            }

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(token);

            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var jsonResponse = await GetUserInformation(userId, token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, jsonResponse.UserId),
                new Claim(ClaimTypes.Name, jsonResponse.FirstName),
                new Claim(ClaimTypes.Surname, jsonResponse.LastName),
                new Claim(ClaimTypes.Email, jsonResponse.Email),
                new Claim(JwtRegisteredClaimNames.Sub, jsonResponse.UserId),
            })));
        }
        public static bool IsTokenExpired(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return true;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var exp = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (long.TryParse(exp, out long expSeconds))
            {
                var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
                return expiryDate < DateTimeOffset.UtcNow;
            }

            // If no exp claim or invalid value, treat it as expired
            return true;
        }
        private async Task<ApplicationUserForView> GetUserInformation(string userId, string token)
        {
            // Add the JWT token to the Authorization header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Send a GET request
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{userId}");

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read and process the response content
                ApplicationUserForView responseContent = await response.Content.ReadFromJsonAsync<ApplicationUserForView>();

                return responseContent;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }

}
