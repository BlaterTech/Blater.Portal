using System.Diagnostics.CodeAnalysis;
using Blater.Models.Bases;

namespace Blater.Portal.Demo.Client.Models;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class TestCrud : BaseDataModel
{
    public string Name { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
}