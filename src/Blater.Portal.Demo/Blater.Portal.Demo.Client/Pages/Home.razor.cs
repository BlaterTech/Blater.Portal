using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Blater.Frontend.Services;
using Blater.JsonUtilities;
using Blater.Models;
using Blater.Models.User;
using Blater.Portal.Demo.Client.Models;
using Blater.Query.Extensions;
using Blater.Query.Models;
using Blater.SDK.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Demo.Client.Pages;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public partial class Home
{
    [Inject]
    protected IBlaterDatabaseStoreTEndpoints<TestCrud> Store { get; set; } = null!;

    [Inject]
    protected ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    protected NavigationService NavigationService { get; set; } = null!;
    
    [Inject]
    protected AuthenticationService AuthenticationService { get; set; } = null!;

    private List<TestCrud> TestCruds { get; set; } = [];
    private bool _loading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var blaterAuthState = await AuthenticationService.GetBlaterState();
            Console.WriteLine("BlaterAuthState.UserId: "    +blaterAuthState?.UserId);
            Console.WriteLine("BlaterAuthState.JwtToken: "  +blaterAuthState?.JwtToken);
            Console.WriteLine("BlaterAuthState JsonValue: " +blaterAuthState.ToJson());
        
            Expression<Func<TestCrud, bool>> predicate = x => x.Name != string.Empty;
            var query = predicate.ExpressionToBlaterQuery();

            BlaterHttpClient.Token = blaterAuthState?.JwtToken;

            var results = await Store.FindMany(query);
            if (results.HandleErrors(out var errorsFind, out var response))
            {
                foreach (var error in errorsFind)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                }
            }
            
            TestCruds = response.ToList();

            if (TestCruds.Count == 0)
            {
                var testCrud = new TestCrud
                {
                    Enabled = true,
                    Name = "Test",
                    Quantity = 1,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                var add = await Store.Insert(testCrud);

                if (add.HandleErrors(out var errors, out var value))
                {
                    foreach (var error in errors)
                    {
                        Snackbar.Add(error.Message, Severity.Error);
                    }

                    return;
                }

                TestCruds.Add(value);
            }
            
            await InvokeAsync(StateHasChanged);
        
            await GetChangesQuery(query);

            TestCruds = TestCruds.OrderByDescending(x => x.Id.GuidValue).ToList();
            _loading = false;
            Console.WriteLine("_loading: "+_loading);
            Console.WriteLine(TestCruds.ToJson());
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task GetChangesQuery(BlaterQuery query)
    {
        Console.WriteLine("Chegou aqui");
        var changes = Store.GetChangesQuery(query);
        await foreach (var change in changes)
        {
            if (change.HandleErrors(out var errors, out var value))
            {
                foreach (var error in errors)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                }

                await InvokeAsync(StateHasChanged);
                continue;
            }

            var index = TestCruds.FindIndex(p => p.Id == value.Id);

            if (index == -1)
            {
                TestCruds.Add(value);
                Snackbar.Add($"{value.Name} was added", Severity.Success);
                await InvokeAsync(StateHasChanged);
                continue;
            }

            if (TestCruds[index].Id.Partition.Equals(value.Id.Partition))
            {
                await InvokeAsync(StateHasChanged);
                continue;
            }

            TestCruds[index] = value;
            Snackbar.Add($"{value.Name} was updated", Severity.Success);
            await InvokeAsync(StateHasChanged);
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
}