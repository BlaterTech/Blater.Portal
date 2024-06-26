using System.Net.Http.Headers;
using System.Text;
using Blater;
using Blater.Exceptions;
using Blater.Interfaces;
using Blater.Interfaces.BlaterAuthentication.Stores;
using Blater.Portal.Apps;
using Blater.Portal.Client.Services;
using Blater.SDK.Implementations.BlaterAuthentication.Stores;
using Blater.SDK.Implementations.BlaterDatabase.Stores;
using Blater.SDK.Implementations.BlaterKeyValue.Stores;
using MudBlazor.Services;
using IBlaterAuthEmailStore = Blater.SDK.Interfaces.IBlaterAuthEmailStore;
using IBlaterAuthLoginStore = Blater.SDK.Interfaces.IBlaterAuthLoginStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddScoped<BrowserViewportObserverService>();

builder.Services.AddHttpClient<BlaterHttpClient>((services, client) =>
{
    var connectionString = services
                          .GetRequiredService<IConfiguration>()
                          .GetConnectionString("CouchDB");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new BlaterException("ConnectionString is nullable");
    }
            
    client.BaseAddress = new Uri(connectionString);

    const string userName = "test";
    const string userPassword = "test";
    const string authenticationString = $"{userName}:{userPassword}";
    var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
});

builder.Services.AddScoped<IBlaterDatabaseStore, BlaterDatabaseStoreEndPoints>();
builder.Services.AddScoped<IBlaterKeyValueStore, BlaterKeyValueStoreEndPoints>();
builder.Services.AddScoped(typeof(IBlaterDatabaseStoreT<>), typeof(BlaterDatabaseStoreTEndPoints<>));
builder.Services.AddScoped<IBlaterAuthEmailStore, BlaterAuthEmailStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthLoginStore, BlaterAuthLoginStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthLockoutStore, BlaterAuthLockoutStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthTwoFactorStore, BlaterAuthTwoFactorStoreEndPoints>();
//builder.Services.AddScoped<IBlaterAuthPasswordStore, password>();
builder.Services.AddScoped<IBlaterAuthPermissionRoleStore, BlaterAuthPermissionRoleStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthPermissionStore, BlaterAuthPermissionStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthRoleStore, BlaterAuthRoleStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthUserRoleStore, BlaterAuthUserRoleStoreEndPoints>();
builder.Services.AddScoped<IBlaterAuthUserPermissionStore, BlaterAuthUserPermissionStoreEndPoints>();

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

app.UseStaticFiles();
app.UseAntiforgery();

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