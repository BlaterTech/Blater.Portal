using System.Diagnostics.CodeAnalysis;
using Blater.Extensions;
using Blater.Frontend.EasyRenderTree;
using Blater.Frontend.Services;
using Blater.Models;
using Blater.Models.User;
using Blater.Portal.Demo.Client.Models;
using Blater.SDK.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Demo.Client.Pages.CRUD;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class FormTestCrud
{
    [Parameter]
    public string? Id { get; set; }
    
    [Inject]
    protected IBlaterDatabaseStoreTEndpoints<TestCrud> Store { get; set; } = null!;
    
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    protected NavigationService NavigationService { get; set; } = null!;

    [Inject]
    protected BlaterAuthState BlaterAuthState { get; set; } = null!;
    
    private TestCrud TestCrud { get; set; } = new();

    private bool Created => string.IsNullOrWhiteSpace(Id);
    private string Title { get; set; } = string.Empty;
    private bool _success;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            BlaterHttpClient.Token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjAyOTQwNjIsImlhdCI6MTcyMDIwNzY2MiwiVXNlcklkIjoiYmxhdGVyX21vZGVsc191c2VyX2JsYXRlcnVzZXI6MDhkYzljZjYtYzcyYS0xNjc5LTUwZjgtNzNjYjcwZGJhYTgyIiwiRW1haWwiOiJ0ZXN0ZSIsIkF2YXRhclVybCI6IiIsIkxvY2tvdXRFbmFibGVkIjoiZGlzYWJsZWQiLCJOYW1lIjoidGVzdGUiLCJwZXJtaXNzaW9ucyI6WyJQcm9qZWN0T3duZXI6RGVsZXRlIiwiUHJvamVjdE93bmVyOlVwZGF0ZSIsIlByb2plY3RPd25lcjpVcHNlcnQiLCJQcm9qZWN0T3duZXI6UmVhZCIsIlByb2plY3RPd25lcjpDcmVhdGUiLCJQcm9qZWN0T3duZXI6T3duZXIiXSwicm9sZSI6IlByb2plY3RPd25lciIsIm5iZiI6MTcyMDIwNzY2Mn0.4AYZYcV8A8GmPLmW-VoxQw2ntbASf8-H7nd82D-3fNE";
            if (!Created)
            {
                var result = await Store.FindOne(x => x.Id == Id!);
                if (result.HandleErrors(out var blaterErrors, out var response))
                {
                    foreach (var error in blaterErrors)
                    {
                        Snackbar.Add(error.Message, Severity.Error);
                    }
                    return;
                }

                Title = $"Editing item: {response.Name}";
                TestCrud = response;
                StateHasChanged();
                return;
            }

            Title = "Creating new product";
            StateHasChanged();
        }
    }

    public async Task Save()
    {
        if (Created)
        {
            var partition = typeof(TestCrud).FullName!.SanitizeString();
            TestCrud.Id = BlaterId.New(partition); 
            TestCrud.CreatedAt = DateTimeOffset.UtcNow;  
        }
        
        TestCrud.UpdatedAt = DateTimeOffset.UtcNow;
        TestCrud.Enabled = true;
        
        BlaterHttpClient.Token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MjAyOTQwNjIsImlhdCI6MTcyMDIwNzY2MiwiVXNlcklkIjoiYmxhdGVyX21vZGVsc191c2VyX2JsYXRlcnVzZXI6MDhkYzljZjYtYzcyYS0xNjc5LTUwZjgtNzNjYjcwZGJhYTgyIiwiRW1haWwiOiJ0ZXN0ZSIsIkF2YXRhclVybCI6IiIsIkxvY2tvdXRFbmFibGVkIjoiZGlzYWJsZWQiLCJOYW1lIjoidGVzdGUiLCJwZXJtaXNzaW9ucyI6WyJQcm9qZWN0T3duZXI6RGVsZXRlIiwiUHJvamVjdE93bmVyOlVwZGF0ZSIsIlByb2plY3RPd25lcjpVcHNlcnQiLCJQcm9qZWN0T3duZXI6UmVhZCIsIlByb2plY3RPd25lcjpDcmVhdGUiLCJQcm9qZWN0T3duZXI6T3duZXIiXSwicm9sZSI6IlByb2plY3RPd25lciIsIm5iZiI6MTcyMDIwNzY2Mn0.4AYZYcV8A8GmPLmW-VoxQw2ntbASf8-H7nd82D-3fNE";
        
        var result = await Store.Upsert(TestCrud);
        if (result.HandleErrors(out var blaterErrors, out var response))
        {
            foreach (var error in blaterErrors)
            {
                Snackbar.Add(error.Message, Severity.Error);
            }
            return;
        }

        Snackbar.Add($"Product {response.Name} saved", Severity.Success);
        NavigationService.Navigate("home");
    }
}