using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Models.Bases;
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
        
        return;

        void Asd(string value)
        {
            //todo: pensar em algo nesse ponto, para dps de atualizar devolver o valor para tela
            Console.WriteLine($"Asd foi chamado com valor: {value}");
            Name += " test";
            
            builder.NotifyStateChanged();
        }
    }
}