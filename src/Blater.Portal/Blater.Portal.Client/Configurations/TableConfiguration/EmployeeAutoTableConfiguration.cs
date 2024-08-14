using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Portal.Client.Models;

namespace Blater.Portal.Client.Configurations.TableConfiguration;

public class EmployeeAutoTableConfiguration : IAutoTableConfiguration<Employee>
{
    public void Configure(AutoTableConfigurationBuilder<Employee> builder)
    {
        builder.Table("Employees");

        builder.Column(x => x.Position, configurationBuilder =>
        {
            configurationBuilder.Name("");
            configurationBuilder.Order(1);
        });

        builder.Column(x => x.Position, configurationBuilder =>
        {
            configurationBuilder.Name("");
            configurationBuilder.Order(1);
        });

        builder.Column(x => x.Position, configurationBuilder =>
        {
            configurationBuilder.Name("");
            configurationBuilder.Order(1);
        });

        builder.Column(x => x.Position, configurationBuilder =>
        {
            configurationBuilder.Name("");
            configurationBuilder.Order(1);
        });
    }
}