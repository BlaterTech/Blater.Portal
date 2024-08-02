using Blater.Models.Bases;

namespace Blater.Portal.Client.Models;

public class Employee : BaseDataModel
{
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int YearsEmployed { get; set; }
    public int Salary { get; set; }
    public int Rating { get; set; }
}