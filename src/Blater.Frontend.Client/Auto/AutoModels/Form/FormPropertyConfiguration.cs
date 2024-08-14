using Blater.Frontend.Client.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration<T> : BasePropertyConfiguration<T>
{
    public AutoFormScope FormScope { get; set; } = AutoFormScope.All;
}