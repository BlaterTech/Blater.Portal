using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Portal.Client.Models;

namespace Blater.Portal.Client.Configurations;

public class EmployeeAutoConfiguration : IAutoConfiguration<Employee>
{
    public void Configure(AutoTableConfigurationBuilder<Employee> builder)
    {
        builder.Table("Employees", configurationBuilder =>
        {
            configurationBuilder.AddMember(x => x.Position);
        });
    }

    public void Configure(AutoFormConfigurationBuilder<Employee> builder)
    {
        builder.Form("FormName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Name)
               .Order(1);
        });

        builder.FormGroup("GroupName", configurationBuilder =>
        {
            configurationBuilder.AddMember(x => x.Name);
            configurationBuilder.AddMember(x => x.Name);
            configurationBuilder.AddMember(x => x.Name);
        });
    }
}