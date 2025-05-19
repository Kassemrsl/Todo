using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Todo.OnlineTaskManagement.BlazorApp.Application.Gateways;
using Todo.OnlineTaskManagement.BlazorApp.Infrastructure.Gateways;
using Todo.OnlineTaskManagement.Web;
using Todo.OnlineTaskManagement.Web.Identity;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAuthGateway, AuthGateway>();
builder.Services.AddScoped<ITasksGateway, TasksGateway>();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7189") });

await builder.Build().RunAsync();
