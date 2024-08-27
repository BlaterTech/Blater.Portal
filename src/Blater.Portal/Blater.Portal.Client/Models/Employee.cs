using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Enumerations;
using Blater.Frontend.Client.Models.Bases;
using Blater.Models.Bases;
using FluentValidation;
using MudBlazor;

namespace Blater.Portal.Client.Models;

public class Employee : BaseFrontendModel
{
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }


    public override void Configure(AutoComponentConfigurationBuilder builder)
    {
        builder.Table("TableName", configurationBuilder =>
        {
            configurationBuilder.AddMember(() => Salary);
        });

        builder.Form("FormName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => Name)
               .Validate(initial => initial.NotEmpty())
               .Order(1);

            configurationBuilder.AddGroup("GroupName", AutoFormGroupScope.Create, groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .LabelName("");
                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .LabelName("");
                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .LabelName("");
            });
        });

        builder.Details("DetailsName", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => Name)
               .Breakpoint(Breakpoint.Lg, 12);

            configurationBuilder.AddGroup("GroupName", false, groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .ComponentType("");

                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .ComponentType("");

                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .ComponentType("");
            });
        });
    }
}