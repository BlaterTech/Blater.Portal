using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.Models;
using Blater.Portal.Demo.Client.Components.Modals;
using Blater.Portal.Demo.Client.Models;
using Blater.Query.Extensions;
using Blater.Query.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Demo.Client.Pages;


public partial class Index
{
    [Inject]
    protected IBlaterDatabaseStoreT<TestCrud> Store { get; set; } = null!;

    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    protected IDialogService DialogService { get; set; } = null!;

    private List<TestCrud> TestCruds { get; set; } = [];
    private bool _loading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Console.WriteLine("Loading");
            Expression<Func<TestCrud, bool>> predicate = x => x.Name != string.Empty;
            var query = predicate.ExpressionToBlaterQuery();
            query.Limit = 100;

            var results = await Store.FindMany(query);
            if (results.HandleErrors(out var errorsFind, out var response))
            {
                foreach (var error in errorsFind)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                }

                _loading = false;
                StateHasChanged();
                return;
            }
            
            TestCruds = response.OrderByDescending(x => x.Id.GuidValue).ToList();
            
            _loading = false;
            StateHasChanged();
        }
    }

    protected override Task OnInitializedAsync()
    {
        InvokeAsync(async () => await GetChangesQuery());
        return base.OnInitializedAsync();
    }

    private async Task GetChangesQuery()
    {
        /*Console.WriteLine("Chegou aqui");
        var changes = Store.GetChangesQuery(query);
        await foreach (var change in changes)
        {
            if (change.HandleErrors(out var errors, out var value))
            {
                foreach (var error in errors)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                }

                StateHasChanged();
                continue;
            }

            var index = TestCruds.FindIndex(p => p.Id == value.Id);

            if (index == -1)
            {
                TestCruds.Add(value);
                Snackbar.Add($"{value.Name} was added", Severity.Success);
                StateHasChanged();
                continue;
            }

            if (TestCruds[index].Id.Partition.Equals(value.Id.Partition))
            {
                StateHasChanged();
                continue;
            }

            TestCruds[index] = value;
            Snackbar.Add($"{value.Name} was updated", Severity.Success);
            StateHasChanged();
        }*/
        
        //Gambiarra
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(500));
        while (true)
        {
            await timer.WaitForNextTickAsync();
            Expression<System.Func<TestCrud, bool>> predicate = x => x.Id != null!;
            var query = predicate.ExpressionToBlaterQuery();
            var changes = await Store.FindMany(query);
            if (changes.HandleErrors(out var errors, out var value))
            {
                foreach (var error in errors)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                }

                StateHasChanged();
                continue;
            }

            TestCruds = value.OrderByDescending(x => x.Id.GuidValue).ToList();
            StateHasChanged();
        }
    }

    private async Task Delete(BlaterId id)
    {
        var result = await Store.Delete(id);
        if (result.HandleErrors(out var errors, out var value))
        {
            foreach (var error in errors)
            {
                Snackbar.Add(error.Message, Severity.Error);
            }

            return;
        }

        var testCrud = TestCruds.FirstOrDefault(x => x.Id == id);
        if (testCrud != null && value)
        {
            TestCruds.Remove(testCrud);
        }

        Snackbar.Add(value ? "Item removed" : "Item not removed", value ? Severity.Success : Severity.Warning);
        StateHasChanged();
    }
    
    private async Task Create()
    {
        var parameters = new DialogParameters();
        await DialogService.ShowAsync<FormTestCrud>("Create", parameters);
    }
    
    private async Task Edit(BlaterId id)
    {
        var parameters = new DialogParameters
        {
            {"Id", id}
        };
        await DialogService.ShowAsync<FormTestCrud>("Edit", parameters);
    }
}