using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using Blater.Frontend.Client.Auto.Interfaces.Validator;
using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Client.Models;

public class Employee :
    BaseFrontendModel,
    IAutoFormConfiguration,
    IAutoValidatorConfiguration<Employee>
{
    private AutoValidatorBuilder<Employee> _configuration;
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    /*public override void Configure(AutoModelConfigurationBuilder<Employee> builder)
    {
        _formActionConfiguration = new AutoFormActionConfiguration
        {
            ColorCancelButton = Color.Dark,
        };

        builder.Form(_formActionConfiguration, configurationBuilder =>
        {
            configurationBuilder.Actions(actionConfigurationBuilder => { actionConfigurationBuilder.TypeCreateEditButton(ButtonType.Submit); });

            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => Name, componentConfigurationBuilder =>
                    {
                        componentConfigurationBuilder
                           .Placeholder("Name")
                           .HelpMessage("Name")
                           .Placeholder("asda")
                           .IsReadOnly(true)
                           .LabelName("Name")
                           .Validate(initial =>
                            {
                                initial.NotEmpty();

                            })
                           .OnValueChanged(NameChanged);
                    });

                groupConfigurationBuilder
                   .AddMember(() => Position, componentConfigurationBuilder =>
                    {
                        componentConfigurationBuilder
                           .Placeholder("Position")
                           .HelpMessage("Position")
                           .IsReadOnly(true)
                           .LabelName("Position")
                           .OnValueChanged(PositionChanged);
                    });

                groupConfigurationBuilder
                   .AddMember(() => Position, config);
            });

            configurationBuilder.AddGroup(groupConfigurationBuilder =>
            {
                groupConfigurationBuilder
                   .AddMember(() => YearsEmployed, componentConfigurationBuilder =>
                    {
                        componentConfigurationBuilder
                           .Placeholder("YearsEmployed")
                           .HelpMessage("YearsEmployed")
                           .IsReadOnly(true)
                           .Validate(initial =>
                            {
                                initial.LessThanOrEqualTo(5);
                                initial.GreaterThan(1);
                            })
                           .LabelName("YearsEmployed");
                    });

                groupConfigurationBuilder
                   .AddMember(() => Salary, componentConfigurationBuilder =>
                    {
                        componentConfigurationBuilder
                           .Placeholder("Salary")
                           .HelpMessage("Salary")
                           .IsReadOnly(true)
                           .LabelName("Salary");
                    });

                groupConfigurationBuilder
                   .AddMember(() => Rating, componentConfigurationBuilder =>
                    {
                        componentConfigurationBuilder
                           .Placeholder("Rating")
                           .HelpMessage("Rating")
                           .IsReadOnly(true)
                           .LabelName("Rating");
                    });
            });
        });
    }
    */


    public void NameChanged(string value)
    {
        Name = $"{value} + test = {value}test";

        StateNotifierService.NotifyStateChanged(() => Name);
    }

    public void PositionChanged(string value)
    {
        Position = $"{value} + test = {value}test";

        StateNotifierService.NotifyStateChanged(() => Position);
    }

    public AutoFormConfiguration Configuration { get; set; } = new()
    {
        Title = "asdasdas"
    };

    public void Configure(AutoFormConfigurationBuilder builder)
    {
        builder
           .AddGroup(new AutoFormGroupConfiguration
            {
                Title = "FormGroupName"
            })
           .AddMember(() => Name, new AutoFormAutoComponentConfiguration
            {
                LabelName = "Name",
                Placeholder = "Insert Name Value",
                OnValueChanged = EventCallback.Factory.Create<string>(this,
                                                                      NameChanged),
            })
           .AddMember(() => Position, new AutoFormAutoComponentConfiguration
            {
                LabelName = "Position",
                Placeholder = "Insert Position Value",
                OnValueChanged = EventCallback.Factory.Create<string>(this, PositionChanged)
            });
    }

    public void Configure(AutoValidatorBuilder<Employee> builder)
    {
        builder.Validate(new AutoValidatorConfiguration<Employee>
        {
            Validators =
            {
                [AutoComponentDisplayType.Form] = new InlineValidator<Employee>
                {
                    v => v.RuleFor(x => x.Name).NotEmpty()
                }
            }
        });
    }
}