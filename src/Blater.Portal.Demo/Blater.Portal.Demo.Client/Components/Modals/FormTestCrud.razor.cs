using Blater.Extensions;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.Models;
using Blater.Portal.Demo.Client.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Demo.Client.Components.Modals;

public partial class FormTestCrud
{
    [Parameter]
    public BlaterId? Id { get; set; }
    
    [Inject]
    protected IBlaterDatabaseStoreT<TestCrud> Store { get; set; } = null!;
    
    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    protected AuthenticationService AuthenticationService { get; set; } = null!;
    
    private TestCrud TestCrud { get; set; } = new();
    
    private string Title { get; set; } = string.Empty;
    
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (Id != null)
        {
            var result = await Store.FindOne(x => x.Id == Id);
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

    public async Task Save()
    {
        TestCrud.UpdatedAt = DateTimeOffset.UtcNow;
        TestCrud.Enabled = true;
            
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
        
        MudDialog.Close(DialogResult.Ok(response));
    }
}