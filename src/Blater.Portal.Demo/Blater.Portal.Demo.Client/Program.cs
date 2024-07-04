using Blater.Frontend.Interfaces;
using Blater.Frontend.Services;
using Blater.Logging;
using Blater.Models.User;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using MudBlazor.Services;

#pragma warning disable CA2252

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.SetupSerilog();

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

builder.Services.AddBlaterServices();

builder.Services.AddScoped<BlaterAuthState>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<NavigationService>();
builder.Services.AddScoped<IBlaterLocalStorageService, BlaterLocalStorageService>();

builder.Services.AddSingleton<LocalizationService>();

var app = builder.Build();

try
{
    await app.RunAsync();
}
finally
{
    await app.DisposeAsync();
}

#pragma warning restore CA2252