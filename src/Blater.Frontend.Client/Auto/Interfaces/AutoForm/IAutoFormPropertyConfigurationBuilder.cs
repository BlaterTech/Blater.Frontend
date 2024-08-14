using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormPropertyConfigurationBuilder<T, TProperty> : IAutoPropertyConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    IAutoFormPropertyConfigurationBuilder<T, TProperty> LabelName(string value);
    IAutoFormPropertyConfigurationBuilder<T, TProperty> Placeholder(string value);
    IAutoFormPropertyConfigurationBuilder<T, TProperty> HelpMessage(string value);
    IAutoFormGroupConfigurationBuilder<T, TProperty> Breakpoint(Breakpoint breakpoint, int value);
}