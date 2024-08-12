using Blater.Frontend.Client.Auto.Components.AutoTable.Builders;
using Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;
using Blater.Portal.Client.Models;

namespace Blater.Portal.Client.TableConfiguration;

public class EmployeeTableConfiguration : ITableConfiguration<Employee>
{
    public void Configure(TableConfigurationBuilder<Employee> builder)
    {
        builder
           .ToTable("Employees");

        builder
           .Property(x => x.Name)
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
           .DisableColumn(true);
        builder
           .Property(x => x.CreatedAt)
           .DisableColumn(true);
        builder
           .Property(x => x.UpdatedAt)
           .DisableColumn(true);
        builder
           .Property(x => x.Enabled)
           .DisableColumn(true);
        builder
           .Property(x => x.Deleted)
           .DisableColumn(true);
    }
}