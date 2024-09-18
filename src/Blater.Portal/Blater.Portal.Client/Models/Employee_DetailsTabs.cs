using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoDetailsTabsConfiguration<Employee>
{
    public AutoDetailsTabsConfiguration<Employee> DetailsTabsConfiguration { get; set; } = new("Employee AutoDetailsTabs");
    public void ConfigureDetailsTabs(AutoDetailsTabsConfigurationBuilder<Employee> builder)
    {
        builder.AddPanel("", configurationBuilder =>
        {
            configurationBuilder.AddGroup("", propertyConfigurationBuilder =>
            {
                propertyConfigurationBuilder.AddMember(x => x.Position, new AutoDetailsTabsPropertyConfiguration<Employee, string>());
            });
        });
    }
}