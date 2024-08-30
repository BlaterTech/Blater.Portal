using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Models.Bases;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Models;

public class Employee : BaseFrontendModel
{
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }


    public override void Configure(AutoModelConfigurationBuilder builder)
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
                   .AddMember(() => Name)
                   .Placeholder("Name")
                   .HelpMessage("Name")
                   .IsReadOnly(true)
                   .LabelName("Name")
                   .OnValueChanged(Asd);
            });
        });

        builder.Details("DetailsName", configurationBuilder =>
        {
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

    public void Asd(string value)
    {
        Name += " test";
    }
}