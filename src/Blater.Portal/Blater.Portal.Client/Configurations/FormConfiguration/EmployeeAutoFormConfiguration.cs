using Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Form;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Portal.Client.Models;
using FluentValidation;

namespace Blater.Portal.Client.Configurations.FormConfiguration;

public class EmployeeAutoFormConfiguration : IAutoFormConfiguration<Employee>
{
    public void Configure(AutoFormConfigurationBuilder<Employee> builder)
    {
       builder.Property(x => x.Name);
       
       
       //validations
       builder.RuleFor(x => x.Name).NotEmpty().WithMessage("Não pode ser null");
    }
}