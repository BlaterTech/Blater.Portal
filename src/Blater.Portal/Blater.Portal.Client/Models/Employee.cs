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
            configurationBuilder.ConfigureActions(actionConfigurationBuilder => { actionConfigurationBuilder.TypeCreateEditButton(ButtonType.Submit); });

            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder.AddMember(() => Name, componentConfigurationBuilder =>
                {
                    componentConfigurationBuilder
                       .Placeholder("Name")
                       .HelpMessage("Name")
                       .IsReadOnly(true)
                       .LabelName("Name")
                       .OnValueChanged(Asd);
                });
            });
        });
    }
    
    public void Asd(string value)
    {
        Console.WriteLine($"Asd foi chamado com valor: {value}");
        Name = $"{value} + test = {value}test";
            
        StateNotifierService<string>.NotifyStateChanged(Name);
    }
}