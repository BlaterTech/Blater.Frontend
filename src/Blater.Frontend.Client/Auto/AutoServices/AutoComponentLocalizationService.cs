using System.Reflection;
using Blater.Frontend.Client.Auto.AutoInterfaces;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoServices;

public class AutoComponentLocalizationService<TModelType>(ILocalizationService localizationService) : IAutoComponentLocalizationService<TModelType>
    where TModelType : BaseDataModel
{
    public required AutoComponentDisplayType DisplayType { get; set; }

    private readonly string _modelTypeName = typeof(TModelType).Name;

    public string GetLabelNameValue(string value, PropertyInfo propertyInfo)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = localizationService.GetValue($"Blater-Auto{DisplayType}-{_modelTypeName}-LabelName-{propertyInfo.Name}");
        }

        return value;
    }

    public string GetTitleValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = localizationService.GetValue($"Blater-Auto{DisplayType}-{_modelTypeName}-Title");
        }

        return value;
    }

    public string GetSubTitleValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = localizationService.GetValue($"Blater-Auto{DisplayType}-{_modelTypeName}-SubTitle");
        }

        return value;
    }

    public string GetGroupNameValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = localizationService.GetValue($"Blater-Auto{DisplayType}-{_modelTypeName}-GroupName");
        }

        return value;
    }

    public string GetPlaceholderValue(string value, PropertyInfo propertyInfo)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            value = localizationService.GetValue($"Blater-Auto{DisplayType}-{_modelTypeName}-Placeholder-{propertyInfo.Name}");
        }

        return value;
    }
}