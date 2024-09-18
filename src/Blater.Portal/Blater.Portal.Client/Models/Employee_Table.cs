using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoTableConfiguration<Employee>
{
    public AutoTableConfiguration<Employee> TableConfiguration { get; set; } = new("Employee Table");

    public void ConfigureTable(AutoTableConfigurationBuilder<Employee> builder)
    {
        var asd = builder
           .AddMemberWithEvent(x => x.Position, new AutoTablePropertyConfiguration<Employee, string>())
           .AddOnValueChanged(x => Position = x);
    }
}