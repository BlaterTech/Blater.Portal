using Blater.Frontend.Client.Auto.AutoTable.Implementations;
using Blater.Frontend.Client.Auto.AutoTable.Interfaces;
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
           .DisabledColumn();
        builder
           .Property(x => x.CreatedAt)
           .DisabledColumn();
        builder
           .Property(x => x.UpdatedAt)
           .DisabledColumn();
        builder
           .Property(x => x.Enabled)
           .DisabledColumn();
        builder
           .Property(x => x.Deleted)
           .DisabledColumn();
    }
}