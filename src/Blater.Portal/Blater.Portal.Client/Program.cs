using Blater.Portal.Client.Services;
using Blater.SDK.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped<BrowserViewportObserverService>();

#pragma warning disable CA2252
builder.Services.AddBlaterServices();
#pragma warning restore CA2252

var app = builder.Build();

await app.RunAsync();