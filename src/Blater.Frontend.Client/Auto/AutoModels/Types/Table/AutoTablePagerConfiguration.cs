using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Table;

public class AutoTablePagerConfiguration
{
    public int[] PageSizeOptions { get; set; } = [10, 25, 50, 100];
    public string RowsPerPageString { get; set; } = "Rows per page:";
    public string InfoFormat { get; set; } = "{first_item}-{last_item} of {all_items}";
    public string AllItemsText { get; set; } = "All";
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Right;
}