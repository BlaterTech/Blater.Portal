using Blater;
using Blater.Portal.Client.Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using Blater.Portal.Components;
using Blater.Portal.Components.Account;
using Blater.Portal.Core;
using Blater.SDK.Extensions;
using Blazr.RenderState.Server;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents()
       .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services
       .AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
       .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Login";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.Cookie = new CookieBuilder
            {
                Name = Configuration.CookieAuthName,
                IsEssential = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            };
        });

builder.Services.AddScoped<CookieHandler>();

builder.Services
       .AddHttpClient<BlaterHttpClient>((_, client) =>
        {
            client.BaseAddress = new Uri("http://localhost:5296");
        })
       .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddBlaterDatabase();
builder.Services.AddBlaterManagement();
builder.Services.AddBlaterKeyValue();
builder.Services.AddBlaterAuthStores();
builder.Services.AddBlaterAuthRepositories();

builder.Services.AddMudServices();
builder.AddBlazrRenderStateServerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Blater.Portal.Client._Imports).Assembly);

try
{
    await app.StartAsync();
    await app.WaitForShutdownAsync();
}
finally
{
    await app.DisposeAsync();
}