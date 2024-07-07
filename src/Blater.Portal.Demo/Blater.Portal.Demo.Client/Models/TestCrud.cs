using System.Diagnostics.CodeAnalysis;
using Blater.Models.Bases;

namespace Blater.Portal.Demo.Client.Models;


public class TestCrud : BaseDataModel
{
    public string Name { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
}