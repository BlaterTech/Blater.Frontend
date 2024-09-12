using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoInterfaces;

public interface IAutoComponentLocalizationService<TModelType>
{
    AutoComponentDisplayType DisplayType { get; set; }
    string GetLabelNameValue(string value, PropertyInfo propertyInfo);
    string GetTitleValue(string value);
    string GetSubTitleValue(string value);
    string GetGroupNameValue(string value);
    string GetPlaceholderValue(string value, PropertyInfo propertyInfo);
}