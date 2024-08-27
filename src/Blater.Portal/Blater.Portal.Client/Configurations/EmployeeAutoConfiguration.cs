using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Enumerations;
using Blater.Portal.Client.Models;
using MudBlazor;

namespace Blater.Portal.Client.Configurations;

public class EmployeeAutoConfiguration : IAutoConfiguration<Employee>
{
    public void Configure(AutoComponentConfigurationBuilder<Employee> builder)
    {
        builder.Table("TableName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Position);
        });
        
        builder.Form("FormName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Name)
               .Order(1);

            configurationBuilder.AddGroup("GroupName", AutoFormGroupScope.Create, groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .LabelName("");
                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .LabelName("");
                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .LabelName("");
            });
        });
        
        builder.Details("DetailsName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Name)
               .Breakpoint(Breakpoint.Lg, 12);

            configurationBuilder.AddGroup("GroupName", false, groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .ComponentType("");

                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .ComponentType("");

                groupConfigurationBuilder
                   .AddMember(x => x.Name)
                   .ComponentType("");
            });
        });
    }
}