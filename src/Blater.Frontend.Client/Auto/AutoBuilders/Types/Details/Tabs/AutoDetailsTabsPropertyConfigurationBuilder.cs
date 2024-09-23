using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details.Tabs;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details.Tabs;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;

public class AutoDetailsTabsPropertyConfigurationBuilder<TModel>(AutoDetailsTabsGroupConfiguration<TModel> configuration) : IAutoDetailsTabsPropertyConfigurationBuilder<TModel>
{
    public IAutoDetailsTabsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoDetailsTabsPropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        var propertyInfo = expression.GetPropertyInfo();
        
        var index = configuration.Components.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.Components[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.LabelNameLocalizationId ??= $"Blater-AutoDetailsTabs-{typeof(TModel).Name}-Member-LabelName-{propertyInfo.Name}";
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForDetails();
            propertyConfiguration.LabelName ??= propertyInfo.Name;
            configuration.Components.Add(propertyConfiguration);
        }

        return propertyConfiguration;
    }   
}