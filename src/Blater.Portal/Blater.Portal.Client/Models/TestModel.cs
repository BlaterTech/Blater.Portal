using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Blater.Attributes.Auto;
using Blater.AutoModelConfigurations;
using Blater.Enumerations;
using Blater.Enumerations.AutoModel;
using Blater.Models.Bases;

namespace Blater.Portal.Client.Models;


[AutoPage(false, true, true, true, BlaterProjects.Blater)]
public class TestModel : BaseFrontendModel
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public int Stock { get; set; }

    public bool IsInSale { get; set; }

    [Required]
    public DateTime SaleStartDate { get; set; }

    [Required]
    public DateTime SaleEndDate { get; set; }
    
    public bool Featured { get; set; }
    
    public override void Configure(AutoModelConfigurator configurator)
    {
        configurator.Property(() => Name)
                    .TableDisplayType(AutoComponentType.AutoText)
                    .FormDisplayType(AutoFormInputType.AutoTextInput)
                    .DetailsDisplayType(AutoComponentType.AutoText)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => Price)
                    .TableDisplayType(AutoComponentType.AutoMoneyRoundedTable)
                    .DetailsDisplayType(AutoComponentType.AutoMoneyRounded)
                    .FormDisplayType(AutoFormInputType.AutoDecimalInput)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => Description)
                    .FormDisplayType(AutoFormInputType.AutoTextAreaInput)
                    .DetailsDisplayType(AutoComponentType.AutoTextArea)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => Stock)
                    .TableDisplayType(AutoComponentType.AutoNumeric)
                    .FormDisplayType(AutoFormInputType.AutoNumericInput)
                    .DetailsDisplayType(AutoComponentType.AutoNumeric)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => SaleStartDate)
                    .FormDisplayType(AutoFormInputType.AutoDateTimeInput)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => SaleEndDate)
                    .FormDisplayType(AutoFormInputType.AutoDateTimeInput)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => IsInSale)
                    .FormDisplayType(AutoFormInputType.AutoSwitchInput)
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => Featured)
                    .FormDisplayType(AutoFormInputType.AutoSwitchInput)
                    .DetailsDisplayType(AutoComponentType.AutoTextStatus)
                    .AddExtraAttribute("TypeName", "AutoTextStatus-StoreProduct-Featured")
                    .GridType(AutoGridType.FullWidth);
        
        configurator.Property(() => CreatedAt)
                    .TableDisplayType(AutoComponentType.AutoDate);
    }
}