using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoFormConfiguration<Employee>
{
    public AutoFormConfiguration<Employee> FormConfiguration { get; } = new("Employee AutoForm");
    public void ConfigureForm(AutoFormConfigurationBuilder<Employee> builder)
    {
        builder.AddGroup("First Group", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(x => x.Position, new AutoFormPropertyConfiguration<Employee, string>());
            
            //todo: add onvaluechanged
        });
    }
}