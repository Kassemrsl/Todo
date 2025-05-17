using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Todo.OnlineTaskManagement.BlazorApp.Application.Gateways;
using Todo.OnlineTaskManagement.BlazorApp.Components;
using Todo.OnlineTaskManagement.BlazorApp.Identity;
using Todo.OnlineTaskManagement.BlazorApp.Infrastructure.Gateways;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(sp => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7189")
});

builder.Services.AddScoped<IAuthGateway, AuthGateway>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
