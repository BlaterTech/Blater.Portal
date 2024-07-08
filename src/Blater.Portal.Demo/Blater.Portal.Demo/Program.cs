using Blater.Frontend.Auto;
using Blater.Frontend.Interfaces;
using Blater.Frontend.Services;
using Blater.Models.User;
using Blater.Portal.Demo.Apps;
using Blater.SDK.Extensions;
using Blater.SDK.Interfaces;
using Blazored.LocalStorage;
using Microsoft.IdentityModel.Logging;
using MudBlazor;
using MudBlazor.Services;
using Serilog;

#pragma warning disable CA2252
var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog();

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

builder.Services.AddAuthentication().AddCookie();

//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddBlaterServices();

//TODO builder.Services.AddScoped<BlaterAuthState>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<NavigationService>();
builder.Services.AddScoped<IBlaterCookieService, BlaterCookieService>();

builder.Services.AddSingleton<LocalizationService>();

builder.Services
       .AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

var scope = app.Services.CreateScope();
var blaterSdk = scope.ServiceProvider.GetRequiredService<IBlaterSDK>();
await blaterSdk.Login("test", "test");

AutoComponentsBuilders.Initialize();

app.UseAntiforgery();

app.MapStaticAssets();

//app.UseStatusCodePagesWithRedirects("/Error/{0}");

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
   .AddAdditionalAssemblies(typeof(Blater.Portal.Demo.Client._Imports).Assembly);

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