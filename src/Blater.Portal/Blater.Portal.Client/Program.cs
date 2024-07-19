using Blater;
using Blater.Portal.Client;
using Blater.Portal.Client.Handlers;
using Blater.SDK.Extensions;
using Blazr.RenderState.WASM;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddHttpClient<BlaterHttpClient>((_, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5296");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddBlaterDatabase();
builder.Services.AddBlaterManagement();
builder.Services.AddBlaterKeyValue();
builder.Services.AddBlaterAuthStores();
builder.Services.AddBlaterAuthRepositories();

builder.Services.AddMudServices();
builder.AddBlazrRenderStateWASMServices();

var app = builder.Build();

try
{
    await app.RunAsync();
}
finally
{
    await app.DisposeAsync();
}