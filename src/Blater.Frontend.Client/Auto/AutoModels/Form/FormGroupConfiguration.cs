using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration<T> where T : BaseDataModel
{
    public string Title { get; set; } = string.Empty;
    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
}