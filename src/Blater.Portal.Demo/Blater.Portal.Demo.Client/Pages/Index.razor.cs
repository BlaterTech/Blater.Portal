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
            Expression<Func<TestCrud, bool>> predicate = x => x.Name != string.Empty && x.Enabled == true;
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
            await Task.Run(GetChangesQuery).ConfigureAwait(false);
        }
    }

    private async Task GetChangesQuery()
    {
        //Gambiarra
        var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(750));
        while (await timer.WaitForNextTickAsync().ConfigureAwait(false))
        {
            Expression<System.Func<TestCrud, bool>> predicate = x => x.Id != null! && x.Enabled == true;
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

            await InvokeAsync(() =>
            {
                TestCruds = value.OrderByDescending(x => x.CreatedAt).ToList();
                StateHasChanged();
            });
        }
    }

    private async Task Delete(TestCrud testCrud)
    {
        testCrud.Enabled = false;
        var result = await Store.Upsert(testCrud);

        if (result.HandleErrors(out var errors, out var value))
        {
            foreach (var error in errors)
            {
                Snackbar.Add(error.Message, Severity.Error);
            }

            StateHasChanged();
            return;
        }

        Snackbar.Add("Item removed", Severity.Success);
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
            { "Id", id }
        };
        await DialogService.ShowAsync<FormTestCrud>("Edit", parameters);
    }
}