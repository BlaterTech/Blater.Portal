using Blater.Frontend.Client.Auto.AutoModels.Base;
using FluentValidation;

namespace Blater.Portal.Client.Models.Validations;

public class EmployeeValidation : BaseModelValidator<Employee>
{
    public EmployeeValidation()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .Length(1, 100)
           .WithMessage("Nome precisa ser maior que 1 e menor que 100");
    }   
}