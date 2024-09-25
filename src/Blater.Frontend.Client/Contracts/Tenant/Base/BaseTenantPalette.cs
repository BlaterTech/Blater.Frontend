namespace Blater.Frontend.Client.Contracts.Tenant.Base;

public class BaseTenantPalette
{
    public string? Primary { get; set; }
    public string? PrimaryDarken { get; set; }
    public string? PrimaryLighten { get; set; }
    
    public string? Secondary { get; set; }
    public string? SecondaryDarken { get; set; }
    public string? SecondaryLighten { get; set; }
    
    public string? Tertiary { get; set; }
    public string? TertiaryDarken { get; set; }
    public string? TertiaryLighten { get; set; }
    
    public string? DrawerBackground { get; set; }
    public string? DrawerText { get; set; }
    public string? DrawerIcon { get; set; }
    
    public string? AppbarBackground { get; set; }
    public string? AppbarText { get; set; }
    
    public string? Divider { get; set; }
    public string? DividerLight { get; set; }
    
    public string? Background { get; set; }
    public string? BackgroundGray { get; set; }
    public string? Surface { get; set; }
    public string? PrimaryContrastText { get; set; }
    public string? SecondaryContrastText { get; set; }
}