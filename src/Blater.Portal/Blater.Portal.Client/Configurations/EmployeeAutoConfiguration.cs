using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Enumerations;
using Blater.Portal.Client.Models;
using MudBlazor;

namespace Blater.Portal.Client.Configurations;

public class EmployeeAutoConfiguration : IAutoConfiguration<Employee>
{
    public void Configure(AutoTableConfigurationBuilder<Employee> builder)
    {
        builder.Table("TableName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Position);
        });
    }

    public void Configure(AutoFormConfigurationBuilder<Employee> builder)
    {
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
    }

    public void Configure(AutoDetailsConfigurationBuilder<Employee> builder)
    {
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