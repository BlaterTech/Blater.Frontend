using System.Reflection;
using FluentValidation;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels;

public abstract class BasePropertyConfiguration<T>
{
    public PropertyInfo PropertyInfo { get; set; } = null!;
    
    public string DataFormat { get; set; } = string.Empty;
    public string CssClass { get; set; } = string.Empty;
    public string CssStyle { get; set; } = string.Empty;
    public string LocalizationId { get; set; } = string.Empty;
    public string ComponentType { get; set; } = string.Empty;
    public string HelpMessage { get; set; } = string.Empty;
    public string LabelName { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    
    public AbstractValidator<T>? Validator { get; set; }
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
    
    public int Order { get; set; }
    
    public bool IsReadOnly { get; set; }
    public bool Disable { get; set; }
}