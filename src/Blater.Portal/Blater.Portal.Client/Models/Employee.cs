using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Models.Bases;
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
            configurationBuilder.ConfigureActions(actionConfigurationBuilder =>
            {
                actionConfigurationBuilder.TypeCreateEditButton(ButtonType.Submit);
            });
            
            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => Name);
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
                   .AddMember(() => Name);

                groupConfigurationBuilder
                   .AddMember(() => Name);

                groupConfigurationBuilder
                   .AddMember(() => Name);
            });
        });
    }
}