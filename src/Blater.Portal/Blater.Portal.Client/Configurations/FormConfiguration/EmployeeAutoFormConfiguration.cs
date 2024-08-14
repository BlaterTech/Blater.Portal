using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Portal.Client.Models;
using FluentValidation;

namespace Blater.Portal.Client.Configurations.FormConfiguration;

public class EmployeeAutoFormConfiguration : IAutoFormConfiguration<Employee>
{
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