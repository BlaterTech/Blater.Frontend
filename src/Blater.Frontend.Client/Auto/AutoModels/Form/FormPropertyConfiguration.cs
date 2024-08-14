namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration<T> : BasePropertyConfiguration
{
    public string LabelName { get; set; } = $"Blater-AutoLabel-{nameof(T)}";
    public string Placeholder { get; set; } = $"Blater-AutoPlaceholder-{nameof(T)}";
    public string HelpMessage { get; set; } = string.Empty;
    
    public bool IsReadOnly { get; set; }
}