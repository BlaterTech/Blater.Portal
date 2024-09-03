using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using Blater.Frontend.Client.Auto.Interfaces.Details;
using Blater.Frontend.Client.Auto.Interfaces.Form;
using Blater.Frontend.Client.Auto.Interfaces.Table;
using Blater.Frontend.Client.Auto.Interfaces.Validator;
using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Portal.Client.Models;

public class Employee : 
    BaseFrontendModel, 
    IAutoTableConfiguration<Employee>, 
    IAutoFormConfiguration<Employee>, 
    IAutoValidatorConfiguration<Employee>,
    IAutoDetailsConfiguration<Employee>
{
    public string Name { get; set; } = null!;

    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    private AutoFormActionConfiguration _formActionConfiguration = new();

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
        Console.WriteLine($"NameChanged foi chamado com valor: {value}");
        Name = $"{value} + test = {value}test";

        if (Name == "Xablau")
        {
            _formActionConfiguration.ColorCancelButton = Color.Primary;
        }

        StateNotifierService.NotifyStateChanged(() => Name);
    }

    public void PositionChanged(string value)
    {
        Console.WriteLine($"PositionChanged foi chamado com valor: {value}");
        Position = $"{value} + test = {value}test";

        StateNotifierService.NotifyStateChanged(() => Position);
    }

    public void ConfigureTable(AutoTableConfigurationBuilder<Employee> builder)
    {
        throw new NotImplementedException();
    }

    public AutoFormModelConfiguration<Employee> Configuration { get; private set; } = default!;
    public void ConfigureForm(AutoFormConfigurationBuilder<Employee> builder)
    {
        Configuration = new AutoFormModelConfiguration<Employee>
        {
            
        };
        builder.Configure(Configuration);
    }
    
    public void ConfigureDetails(AutoDetailsConfigurationBuilder<Employee> builder)
    {
        throw new NotImplementedException();
    }
    
    public void ConfigureValidator(AutoValidatorBuilder<Employee> builder)
    {
        var inlineValidator = new InlineValidator<Employee>
        {
            v => v.RuleFor(x => x.Name).NotEmpty(),
        };

        builder.Validate(AutoComponentDisplayType.Form, inlineValidator);

        var validator = new AutoValidatorConfiguration<Employee>
        {
            Validators =
            {
                [AutoComponentDisplayType.FormCreate] = new InlineValidator<Employee>
                {
                    v => v.RuleFor(x => x.Name).NotEmpty()
                }
            }
        };
        builder.Validate(validator);
    }
}