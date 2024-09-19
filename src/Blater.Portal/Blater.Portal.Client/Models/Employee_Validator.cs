using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using FluentValidation;

namespace Blater.Portal.Client.Models;

public partial class Employee : IAutoValidatorConfiguration<Employee>
{
    public InlineValidator<Employee> Validator { get; set; } = new()
    {
        v => v.RuleFor(x => x.Name).NotEqual("a").WithMessage("Name not null possible equal 'a'"),
        v => v.RuleFor(x => x.Salary).NotEqual(1).WithMessage("Salary not equal 1")
    };
}