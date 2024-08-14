using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Portal.Client.Models;

namespace Blater.Portal.Client.Configurations.TableConfiguration;

public class EmployeeAutoTableConfiguration : IAutoTableConfiguration<Employee>
{
    public void Configure(AutoTableConfigurationBuilder<Employee> builder)
    {
        builder
           .ToTable("Employees");

        builder
           .Property(x => x.CreatedAt)
           .Name("_name")
           .Order(1);
        builder
           .Property(x => x.Position)
           .Order(2);
        builder
           .Property(x => x.Rating)
           .Order(3);
        builder
           .Property(x => x.Salary)
           .Order(4);
        builder
           .Property(x => x.YearsEmployed)
           .Order(5);

        builder
           .Property(x => x.Id)
           .Disable(true);
        builder
           .Property(x => x.CreatedAt)
           .Disable(true);
        builder
           .Property(x => x.UpdatedAt)
           .Disable(true);
        builder
           .Property(x => x.Enabled)
           .Disable(true);
        builder
           .Property(x => x.Deleted)
           .Disable(true);
    }
}