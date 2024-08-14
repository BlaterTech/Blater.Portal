using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Portal.Client.Models;
using FluentValidation;

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
               .AddPartner(x => x.Name)
               .Order(1)
               .Validate(initial => { initial.NotEmpty(); });
        });

        builder.FormGroup("GroupName", configurationBuilder =>
        {
            configurationBuilder.AddPartner(x => x.Name);
            configurationBuilder.AddPartner(x => x.Name);
            configurationBuilder.AddPartner(x => x.Name);
        });
    }
}