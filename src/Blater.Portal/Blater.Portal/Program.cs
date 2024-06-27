using Blater.Frontend.Auto;
using Blater.Frontend.Logging;
using Blater.Frontend.Services;
using Blater.Models.User;
using Blater.Portal.Apps;
using Blater.Portal.Client.Services;
using Blater.SDK.Extensions;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Logging;
using MudBlazor;
using MudBlazor.Services;

#pragma warning disable CA2252
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

IdentityModelEventSource.ShowPII = true;

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression();
}

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
        
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<BlaterAuthState>();
builder.Services.AddScoped<BrowserViewportObserverService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<NavigationService>();

builder.Services.AddSingleton<LocalizationService>();
        
builder.Services.AddBlaterServices();

builder.Services
       .AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

AutoComponentsBuilders.Initialize();
        
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.UseStatusCodePagesWithRedirects("/Error/{0}");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging();
}

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
#pragma warning restore CA2252