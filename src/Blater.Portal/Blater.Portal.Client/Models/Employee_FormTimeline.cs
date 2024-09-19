using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoFormTimelineConfiguration<Employee>
{
    public AutoFormTimelineConfiguration<Employee> FormTimelineConfiguration { get; set; } = new("Employee FormTimeline");

    public void ConfigureFormTimeline(IAutoFormTimelineConfigurationBuilder<Employee> builder)
    {
        builder.AddStep("Step One", configurationBuilder =>
        {
            configurationBuilder.AddGroup("Group One", propertyConfigurationBuilder =>
            {
                propertyConfigurationBuilder
                   .AddMemberOnly(x => x.Position, new AutoFormPropertyConfiguration<Employee, string>());
            });
        });
    }
}