using Blater.Models.User;
using Blater.Portal.Apps;
using Blater.Portal.Client.Services;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<BrowserViewportObserverService>();

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Blater.Portal.Client._Imports).Assembly);

try
{
    await app.RunAsync();
    await app.StopAsync();
}
finally
{
    await app.DisposeAsync();
}