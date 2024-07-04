using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Services;
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

    private TestCrud TestCrud { get; set; } = new();

    private bool Created => string.IsNullOrWhiteSpace(Id);
    private string Title { get; set; } = string.Empty;
    bool success;
    string[] errors = { };
    MudForm form;

    protected override async Task OnInitializedAsync()
    {
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
            return;
        }

        Title = "Creating new product";
    }

    public async Task Save()
    {
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