using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration<T> where T : BaseDataModel
{
    public string Title { get; set; } = string.Empty;
    public AutoFormGroupScope FormGroupScope { get; set; } = AutoFormGroupScope.All;
    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
}