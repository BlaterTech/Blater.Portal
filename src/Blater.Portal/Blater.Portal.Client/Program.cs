using Blater.Frontend.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.AddBlaterFrontendClient();

var app = builder.Build();

//app.UseBlaterFrontend<App>();

await app.RunAsync();
