using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Valitador;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Validator;
using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace Blater.Portal.Client.Models;

public class Employee :
    BaseFrontendModel,
    IAutoFormTimelineConfiguration,
    IAutoDetailsConfiguration,
    IAutoTableConfiguration,
    IAutoDetailsTabsConfiguration,
    IAutoValidatorConfiguration<Employee>
{
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    public AutoFormConfiguration FormConfiguration { get; } = new("FormTitle");

    public void Configure(AutoFormConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoFormGroupConfiguration("FirstGroup"), configurationBuilder =>
        {
            configurationBuilder
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
        });

        builder.AddGroup(new AutoFormGroupConfiguration("SecondGroup"), configurationBuilder =>
        {
            configurationBuilder
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
        });
    }

    public AutoFormTimelineConfiguration FormTimelineConfiguration { get; set; } = new("Form Timeline Employee");
    public void Configure(IAutoFormTimelineConfigurationBuilder builder)
    {
        builder.AddStep(new AutoFormTimelineStepConfiguration(1, "First Step"));
        builder.AddStep(new AutoFormTimelineStepConfiguration(2, "Second Step"));
        builder.AddStep(new AutoFormTimelineStepConfiguration(3, "Tertiary Step"));
    }

    public AutoDetailsConfiguration DetailsConfiguration { get; } = new("AutoDetailsTitle");

    public void Configure(AutoDetailsConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoDetailsGroupConfiguration("AutoDetailsGroup"), configurationBuilder =>
        {
            configurationBuilder
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
        });
    }

    public AutoTableConfiguration TableConfiguration { get; } = new("AutoTableTitle");

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
        configurationBuilder.FormValidate(rules =>
        {
            rules.RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            rules.RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0");
        });
    }
    
    public AutoDetailsTabsConfiguration DetailsTabsConfiguration { get; set; } = new()
    {
        Title = "Details Tabs Title"
    };

    public void Configure(AutoDetailsTabsConfigurationBuilder builder)
    {
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel One"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group One"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => Position, new AutoDetailsTabsComponentConfiguration());
                    
                    memberConfigurationBuilder
                       .AddMember(() => Salary, new AutoDetailsTabsComponentConfiguration());
                });
        });
        
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel Two"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group Two"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => YearsEmployed, new AutoDetailsTabsComponentConfiguration());
                });
        });
        
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel Three"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group Three"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => Name, new AutoDetailsTabsComponentConfiguration());
                });
        });
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