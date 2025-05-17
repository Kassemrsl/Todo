using Todo.OnlineTaskManagement.Shared.Requests;

namespace Todo.OnlineTaskManagement.BlazorApp.Application.Gateways
{
    public interface IAuthGateway
    {
        Task<AuthResult> LoginAsync(UserLoginRequest model);

        Task<AuthResult> RegisterAsync(RegistrationDto model);
    }
}
