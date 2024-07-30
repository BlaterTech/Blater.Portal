using Blater;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Services;
using Blater.Frontend.StateManagement;
using Blater.Frontend.StateManagement.Database;
using Blater.Logging;
using Blater.Portal.Client;
using Blater.Portal.Client.Handlers;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.SetupSerilog();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<CookieHandler>();

builder.Services
       .AddHttpClient<BlaterHttpClient>((_, client) => { client.BaseAddress = new Uri("http://localhost:5296"); })
       .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddBlaterDatabase();
builder.Services.AddBlaterManagement();
builder.Services.AddBlaterKeyValue();
builder.Services.AddBlaterAuthStores();
builder.Services.AddBlaterAuthRepositories();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICookieService, CookieService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IBlaterMemoryCache, BlaterMemoryCache>();
builder.Services.AddScoped<IBlaterStateStore, BlaterStateStore>();
builder.Services.AddScoped<IBrowserViewportObserverService, BrowserViewportObserverService>();
builder.Services.AddMudServices();

var app = builder.Build();

try
{
    await app.RunAsync();
}
finally
{
    await app.DisposeAsync();
}