using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Services;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoFormConfiguration<Employee>
{
    public AutoFormConfiguration<Employee> FormConfiguration { get; set; } = new("Employee AutoForm");

    public void ConfigureForm(AutoFormConfigurationBuilder<Employee> builder)
    {
        builder.AddGroup("Group One", configurationBuilder =>
        {
            var memberOne = configurationBuilder
               .AddMemberOnly(x => x.Position, new AutoFormPropertyConfiguration<Employee, string>());

            var memberSecond = configurationBuilder
                              .AddMemberWithEvent(x => x.Name, new AutoFormPropertyConfiguration<Employee, string>())
                              .AddOnValueChanged(x => memberOne.Value = x, memberOne);

            configurationBuilder
               .AddMemberWithEvent(x => x.Test, new AutoFormPropertyConfiguration<Employee, string>())
               .AddOnValueChanged(x =>
                {
                    if (x.Length > 5)
                    {
                        x = $"{x} > 5 char";
                    }
                    return x;
                });
        });

        builder.AddGroup("Group Second", configurationBuilder =>
        {
            var memberOne = configurationBuilder
               .AddMemberOnly(x => x.YearsEmployed, new AutoFormPropertyConfiguration<Employee, int>());

            var memberSecond = configurationBuilder
               .AddMemberOnly(x => x.Rating, new AutoFormPropertyConfiguration<Employee, int>());

            configurationBuilder
               .AddMemberWithEvent(x => x.Salary, new AutoFormPropertyConfiguration<Employee, int>())
               .AddOnValueChanged(x =>
                {
                    memberSecond.Value = x;
                    memberOne.Value = x;
                }, memberOne, memberSecond);
        });
    }
}