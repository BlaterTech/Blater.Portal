using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoDetailsConfiguration<Employee>
{
    public AutoDetailsConfiguration<Employee> DetailsConfiguration { get; } = new("Employee Details");
    public void ConfigureDetails(AutoDetailsConfigurationBuilder<Employee> builder)
    {
        builder.AddGroup("", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Name, new AutoDetailsPropertyConfiguration<Employee, string>());
        });
    }
}