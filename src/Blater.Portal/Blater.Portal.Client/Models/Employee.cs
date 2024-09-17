using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
using Blater.Frontend.Client.Auto.AutoBuilders.Types.Valitador;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
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

public partial class Employee :
    BaseFrontendModel,
    IAutoFormTimelineConfiguration,
    IAutoDetailsConfiguration,
    
    IAutoDetailsTabsConfiguration,
    IAutoValidatorConfiguration<Employee>
{
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    public AutoFormConfiguration FormConfiguration { get; } = new("FormTitle");

    public void ConfigureForm(AutoFormConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoFormGroupConfiguration("FirstGroup"), );

        builder.AddGroup(new AutoFormGroupConfiguration("SecondGroup"), configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => YearsEmployed, new AutoFormAutoPropertyConfiguration
                {
                    LabelName = "YearsEmployed",
                    Placeholder = "Insert YearsEmployed Value"
                })
               .AddMember(() => Salary, new AutoFormAutoPropertyConfiguration
                {
                    LabelName = "Salary",
                    Placeholder = "Insert Salary Value"
                });
        });
    }

    public AutoFormTimelineConfiguration FormTimelineConfiguration { get; set; } = new("Form Timeline Employee");
    public void ConfigureFormTimeline(IAutoFormTimelineConfigurationBuilder builder)
    {
        var step = builder.AddStep("First Step", configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => Name, new AutoFormAutoPropertyConfiguration
                {
                    LabelName = "Name",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, NameChanged),
                })
               .AddMember(() => Position, new AutoFormAutoPropertyConfiguration
                {
                    LabelName = "Position",
                    Placeholder = "Insert Position Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, PositionChanged)
                });

            configurationBuilder
               .AddSubgroup(new AutoFormGroupConfiguration("First SubGroup"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder.AddMember(() => Rating, new AutoFormAutoPropertyConfiguration
                    {
                        LabelName = "Rating",
                        Placeholder = "Insert Rating Value"
                    });
                });
        });

        var group = step.AddGroup("Test", configurationBuilder =>
            {
                configurationBuilder.AddMember(() => Name, new AutoFormAutoPropertyConfiguration<string>
                {
                    LabelName = "Name",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, NameChanged),
                });
                
                var member2 = configurationBuilder.AddMember(() => Position, new AutoFormAutoPropertyConfiguration<string>
                {
                    LabelName = "Position   ",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, s => member.Value = s),
                });
                
                
            }
        );
        
        var groupTest = step.AddGroup(new AutoFormGroupConfiguration("asd"), configurationBuilder =>
        {
            configurationBuilder.AddMember(model => model.Name);

        });
        
        
        builder.AddStep("Second Step");
        builder.AddStep("Tertiary Step");
    }

    public AutoDetailsConfiguration DetailsConfiguration { get; } = new("AutoDetailsTitle");

    public void ConfigureDetails(AutoDetailsConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoDetailsGroupConfiguration("AutoDetailsGroup"), configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => Position, new AutoDetailsAutoPropertyConfiguration
                {
                    LabelName = "Position Detail"
                })
               .AddMember(() => Salary, new AutoDetailsAutoPropertyConfiguration
                {
                    LabelName = "Salary Detail"
                })
               .AddMember(() => Rating, new AutoDetailsAutoPropertyConfiguration
                {
                    LabelName = "Rating Detail"
                });
        });
    }

    public AutoTableConfiguration TableConfiguration { get; } = new("AutoTableTitle");

    public void ConfigureTable(AutoTableConfigurationBuilder builder)
    {
        builder.AddMember(() => Position, new AutoTableAutoPropertyConfiguration
        {
            LabelName = "Position Table"
        });
    }

    public AutoValidatorConfiguration<Employee> ValidatorConfiguration { get; } = new();

    public void ConfigureValidations(AutoValidatorConfigurationBuilder<Employee> configurationBuilder)
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

    public void ConfigureDetailsTabs(AutoDetailsTabsConfigurationBuilder builder)
    {
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel One"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group One"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => Position, new AutoDetailsTabsPropertyConfiguration());
                    
                    memberConfigurationBuilder
                       .AddMember(() => Salary, new AutoDetailsTabsPropertyConfiguration());
                });
        });
        
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel Two"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group Two"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => YearsEmployed, new AutoDetailsTabsPropertyConfiguration());
                });
        });
        
        builder.AddPanel(new AutoDetailsTabsPanelConfiguration("Panel Three"), configurationBuilder =>
        {
            configurationBuilder
               .AddGroup(new AutoDetailsTabsGroupConfiguration("Group Three"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => Name, new AutoDetailsTabsPropertyConfiguration());
                });
        });
    }

    public void NameChanged(string value)
    {
        Name = $"{value} name";

        StateNotifierService.NotifyStateChanged(() => Name, typeof(Employee));
    }

    public void PositionChanged(string value)
    {
        Position = $"{value} position";

        StateNotifierService.NotifyStateChanged(() => Position, typeof(Employee));
    }
}