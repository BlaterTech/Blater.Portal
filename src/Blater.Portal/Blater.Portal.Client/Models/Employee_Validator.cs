using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using FluentValidation;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoValidatorConfiguration<Employee>
{
    public InlineValidator<Employee> Validator { get; } = new()
    {
        v => v.RuleFor(x => x.Name).NotNull(),
    };
}