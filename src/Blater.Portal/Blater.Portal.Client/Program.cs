using Blater.Interfaces.BlaterAuthentication.Repositories;
using Blater.Portal.Client.Services;
using Blater.SDK.Implementations.BlaterAuthentication.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using IBlaterAuthEmailRepository = Blater.SDK.Interfaces.IBlaterAuthEmailRepository;
using IBlaterAuthLoginRepository = Blater.SDK.Interfaces.IBlaterAuthLoginRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped<BrowserViewportObserverService>();

#pragma warning disable CA2252
builder.Services.AddScoped<IBlaterAuthEmailRepository, BlaterAuthEmailRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthLoginRepository, BlaterAuthLoginRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthLockoutRepository, BlaterAuthLockoutRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthTwoFactorRepository, BlaterAuthTwoFactorRepositoryEndPoints>();
//builder.Services.AddScoped<IBlaterAuthPasswordRepository, BlaterAuthPasswordRepository>();
builder.Services.AddScoped<IBlaterAuthPermissionRoleRepository, BlaterAuthPermissionRoleRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthPermissionRepository, BlaterAuthPermissionRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthRoleRepository, BlaterAuthRoleRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthUserRoleRepository, BlaterAuthUserRoleRepositoryEndPoints>();
builder.Services.AddScoped<IBlaterAuthUserPermissionRepository, BlaterAuthUserPermissionsRepositoryEndPoints>();
#pragma warning restore CA2252

var app = builder.Build();

await app.RunAsync();