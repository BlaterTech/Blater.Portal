using Blater.Models.User;
using Blater.Portal.Client.Services;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped<BrowserViewportObserverService>();

builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
});

#pragma warning disable CA2252

builder.Services.AddScoped<BlaterUserToken>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<NavigationService>();

builder.Services.AddBlaterServices();
#pragma warning restore CA2252

var app = builder.Build();

await app.RunAsync();