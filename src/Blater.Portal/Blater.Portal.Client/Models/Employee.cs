using Blater.Frontend.Client.Models.Bases;
using Blater.Frontend.Client.Services;

namespace Blater.Portal.Client.Models;

public partial class Employee : BaseFrontendModel
{
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string Test { get; set; } = null!;
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }

    /*
    public AutoFormConfiguration FormConfiguration { get; set; } = new("FormTitle");

    public void ConfigureForm(AutoFormConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoFormGroupConfiguration("FirstGroup"), );

        builder.AddGroup(new AutoFormGroupConfiguration("SecondGroup"), configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => YearsEmployed, new BaseAutoFormBaseAutoPropertyConfiguration<>
                {
                    LabelName = "YearsEmployed",
                    Placeholder = "Insert YearsEmployed Value"
                })
               .AddMember(() => Salary, new BaseAutoFormBaseAutoPropertyConfiguration<>
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
               .AddMember(() => Name, new BaseAutoFormBaseAutoPropertyConfiguration<>
                {
                    LabelName = "Name",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, NameChanged),
                })
               .AddMember(() => Position, new BaseAutoFormBaseAutoPropertyConfiguration<>
                {
                    LabelName = "Position",
                    Placeholder = "Insert Position Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, PositionChanged)
                });

            configurationBuilder
               .AddSubgroup(new AutoFormGroupConfiguration("First SubGroup"), memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder.AddMember(() => Rating, new BaseAutoFormBaseAutoPropertyConfiguration<>
                    {
                        LabelName = "Rating",
                        Placeholder = "Insert Rating Value"
                    });
                });
        });

        var group = step.AddGroup("Test", configurationBuilder =>
            {
                configurationBuilder.AddMember(() => Name, new BaseAutoFormBaseAutoPropertyConfiguration<string>
                {
                    LabelName = "Name",
                    Placeholder = "Insert Name Value",
                    OnValueChanged = EventCallback.Factory.Create<string>(this, NameChanged),
                });
                
                var member2 = configurationBuilder.AddMember(() => Position, new BaseAutoFormBaseAutoPropertyConfiguration<string>
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

    public AutoDetailsConfiguration DetailsConfiguration { get; set; } = new("AutoDetailsTitle");

    public void ConfigureDetails(AutoDetailsConfigurationBuilder builder)
    {
        builder.AddGroup(new AutoDetailsGroupConfiguration("AutoDetailsGroup"), configurationBuilder =>
        {
            configurationBuilder
               .AddMember(() => Position, new BaseAutoDetailsBaseAutoPropertyConfiguration
                {
                    LabelName = "Position Detail"
                })
               .AddMember(() => Salary, new BaseAutoDetailsBaseAutoPropertyConfiguration
                {
                    LabelName = "Salary Detail"
                })
               .AddMember(() => Rating, new BaseAutoDetailsBaseAutoPropertyConfiguration
                {
                    LabelName = "Rating Detail"
                });
        });
    }

    public void ConfigureTable(AutoTableConfigurationBuilder builder)
    {
        builder.AddMember(() => Position, new BaseAutoTableBaseAutoPropertyConfiguration<>
        {
            LabelName = "Position Table"
        });
    }

    public AutoValidatorConfiguration<Employee> ValidatorConfiguration { get; set; } = new();

    public void ConfigureValidations(AutoValidatorConfigurationBuilder<Employee> configurationBuilder)
    {
        configurationBuilder.RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        configurationBuilder.RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0");
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
               .AddGroup("Group Three", memberConfigurationBuilder =>
                {
                    memberConfigurationBuilder
                       .AddMember(() => Name, new AutoDetailsTabsPropertyConfiguration());
                });
        });
    }
*/
    
    /*public void NameChanged(string value)
    {
        Name = $"{value} name";

        StateNotifierService.NotifyStateChanged(() => Name, typeof(Employee));
    }

    public void PositionChanged(string value)
    {
        Position = $"{value} position";

        StateNotifierService.NotifyStateChanged(() => Position, typeof(Employee));
    }*/
}