using System.Net.Http.Headers;
using Blater;
using Blater.Models.User;
using Blater.Portal.Client.Handlers;
using Blater.SDK.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddHttpClient<BlaterHttpClient>((sp, client) =>
{
    client.BaseAddress = new Uri("http://localhost:5296");
}).AddHttpMessageHandler<CookieHandler>();

builder.Services.AddBlaterDatabase();
builder.Services.AddBlaterManagement();
builder.Services.AddBlaterKeyValue();
builder.Services.AddBlaterAuthStores();
builder.Services.AddBlaterAuthRepositories();

await builder.Build().RunAsync();