using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;
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
        builder.Form("FormName", configurationBuilder =>
        {
            configurationBuilder
               .ConfigureActions(actionConfigurationBuilder => { actionConfigurationBuilder.TypeCreateEditButton(ButtonType.Submit); });

            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => Name)
                   .Placeholder("Name")
                   .HelpMessage("Name")
                   .IsReadOnly(true)
                   .LabelName("Name")
                   .OnValueChanged(NameChanged);

                groupConfigurationBuilder
                   .AddMember(() => Position)
                   .Placeholder("Position")
                   .HelpMessage("Position")
                   .IsReadOnly(true)
                   .LabelName("Position")
                   .OnValueChanged(PositionChanged);

                groupConfigurationBuilder
                   .AddMember(() => YearsEmployed)
                   .Placeholder("YearsEmployed")
                   .HelpMessage("YearsEmployed")
                   .IsReadOnly(true)
                   .LabelName("YearsEmployed");
                
                groupConfigurationBuilder
                   .AddMember(() => Salary)
                   .Placeholder("Salary")
                   .HelpMessage("Salary")
                   .IsReadOnly(true)
                   .LabelName("Salary");
                
                groupConfigurationBuilder
                   .AddMember(() => Rating)
                   .Placeholder("Rating")
                   .HelpMessage("Rating")
                   .IsReadOnly(true)
                   .LabelName("Rating");
            });
        });
    }

    public void NameChanged(string value)
    {
        Console.WriteLine($"NameChanged foi chamado com valor: {value}");
        Name = $"{value} + test = {value}test";
        
        StateNotifierService.NotifyStateChanged(() => Name);
    }

    public void PositionChanged(string value)
    {
        Console.WriteLine($"PositionChanged foi chamado com valor: {value}");
        Position = $"{value} + test = {value}test";
        
        StateNotifierService.NotifyStateChanged(() => Position);
    }
}