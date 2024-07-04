using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Blater.Enumerations;
using Blater.Frontend.Services;
using Blater.Interfaces;
using Blater.JsonUtilities;
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

    private List<TestCrud> TestCruds { get; set; } = [];
    private bool _isCellEditMode;
    private bool _editTriggerRowClick;
    private string Jwt = "";

    protected override async Task OnInitializedAsync()
    {
        var query = new BlaterQuery
        {
            Sort = new List<IDictionary<string, OrderDirection>>
            {
                new Dictionary<string, OrderDirection>
                {
                    { "createdAt", OrderDirection.Descending }
                }
            }
        };

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
            await InvokeAsync(StateHasChanged);
        }

        await foreach (var item in Items())
        {
            TestCruds = item;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async IAsyncEnumerable<List<TestCrud>> Items([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = new BlaterQuery
        {
            Sort = new List<IDictionary<string, OrderDirection>>
            {
                new Dictionary<string, OrderDirection>
                {
                    { "createdAt", OrderDirection.Descending }
                }
            }
        };
        
        var changes = Store.GetChangesQuery(query);
        await foreach (var change in changes.WithCancellation(cancellationToken))
        {
            if (change.HandleErrors(out var errors, out var value))
            {
                foreach (var error in errors)
                {
                    Snackbar.Add(error.Message, Severity.Error);
                    Console.WriteLine(errors.ToJson());
                }
            }
            
            var index = TestCruds.FindIndex(p => p.Id == value.Id);

            if (index == -1)
            {
                TestCruds.Add(value);
                Snackbar.Add($"{value.Name} was added", Severity.Success);
                yield return TestCruds;
            }

            if (TestCruds[index].Id.Partition.Equals(value.Id.Partition))
            {
                yield return TestCruds;
            }

            TestCruds[index] = value;
            Snackbar.Add($"{value.Name} was updated", Severity.Success);
            yield return TestCruds;
        }  
        
    }
    
    
    private List<string> _events = [];
    void StartedEditingItem(TestCrud item)
    {
        _events.Insert(0, $"Event = StartedEditingItem, Data = {item.ToJson()}");
    }

    void CanceledEditingItem(TestCrud item)
    {
        _events.Insert(0, $"Event = CanceledEditingItem, Data = {item.ToJson()}");
    }

    void CommittedItemChanges(TestCrud item)
    {
        _events.Insert(0, $"Event = CommittedItemChanges, Data = {item.ToJson()}");
    }
}