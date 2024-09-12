using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using Blater.Frontend.Client.Auto.Interfaces.Types.Details;
using Blater.Frontend.Client.Auto.Interfaces.Types.Form;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;
using Blater.Frontend.Client.Auto.Interfaces.Types.Validator;
using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Client.Models;

public class Employee :
    BaseFrontendModel,
    IAutoFormConfiguration,
    IAutoDetailsConfiguration,
    IAutoTableConfiguration,
    IAutoValidatorConfiguration<Employee>
{
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    public AutoFormConfiguration FormConfiguration { get; } = new()
    {
        Title = "FormTitle"
    };

    public void Configure(AutoFormConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoFormGroupConfiguration
                {
                    Title = "FirstGroup"
                })
               .AddMember(() => Name, new AutoFormAutoComponentConfiguration
                {
                    LabelName = "Name",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, NameChanged),
                })
               .AddMember(() => Position, new AutoFormAutoComponentConfiguration
                {
                    LabelName = "Position",
                    Placeholder = "Insert Position Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, PositionChanged)
                });

        builder.AddGroup(new AutoFormGroupConfiguration
                {
                    Title = "SecondGroup"
                })
               .AddMember(() => YearsEmployed, new AutoFormAutoComponentConfiguration
                {
                    LabelName = "YearsEmployed",
                    Placeholder = "Insert YearsEmployed Value"
                })
               .AddMember(() => Salary, new AutoFormAutoComponentConfiguration
                {
                    LabelName = "Salary",
                    Placeholder = "Insert Salary Value"
                })
               .AddMember(() => Rating, new AutoFormAutoComponentConfiguration
                {
                    LabelName = "Rating",
                    Placeholder = "Insert Rating Value"
                });
    }

    public AutoDetailsConfiguration DetailsConfiguration { get; } = new()
    {
        Title = "AutoDetailsTitle"
    };

    public void Configure(AutoDetailsConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoDetailsGroupConfiguration
                {
                    Title = "AutoDetailsGroup"
                })
               .AddMember(() => Position, new AutoDetailsAutoComponentConfiguration
                {
                    LabelName = "Position Detail"
                })
               .AddMember(() => Salary, new AutoDetailsAutoComponentConfiguration
                {
                    LabelName = "Salary Detail"
                })
               .AddMember(() => Rating, new AutoDetailsAutoComponentConfiguration
                {
                    LabelName = "Rating Detail"
                });
    }

    public AutoTableConfiguration TableConfiguration { get; } = new()
    {
        Title = "AutoTableTitle"
    };

    public void Configure(AutoTableConfigurationBuilder builder)
    {
        builder.AddMember(() => Position, new AutoTableAutoComponentConfiguration
        {
            LabelName = "Position Table"
        });
    }

    public AutoValidatorConfiguration<Employee> ValidatorConfiguration { get; } = new();

    public void Configure(AutoValidatorConfigurationBuilder<Employee> configurationBuilder)
    {
        var formValidator = new ModelValidator<Employee>();

        formValidator.RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        formValidator.RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0");

        configurationBuilder.FormValidate(formValidator);
    }

    public void NameChanged(string value)
    {
        Name = $"{value} name";
        
        StateNotifierService.NotifyStateChanged(() => Name);
    }

    public void PositionChanged(string value)
    {
        Position = $"{value} position";

        StateNotifierService.NotifyStateChanged(() => Position);
    }
}