using Blater.Frontend;
using Blater.Portal.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddBlaterFrontendServer();

var app = builder.Build();

app.UseBlaterFrontendServer<App>(typeof(Blater.Portal.Client._Imports).Assembly);

app.Run();