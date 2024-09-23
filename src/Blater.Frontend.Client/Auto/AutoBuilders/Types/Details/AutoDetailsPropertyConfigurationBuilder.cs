using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public class AutoDetailsPropertyConfigurationBuilder<TModel>(AutoDetailsGroupConfiguration<TModel> configuration) : IAutoDetailsMemberConfigurationBuilder<TModel>
{
    public IAutoDetailsPropertyConfiguration<TModel> AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoDetailsPropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
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
            propertyConfiguration.LabelNameLocalizationId ??= $"Blater-AutoDetails-{typeof(TModel).Name}-Member-LabelName-{propertyInfo.Name}";
            propertyConfiguration.LabelName ??= propertyInfo.Name;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForDetails();
            configuration.Components.Add(propertyConfiguration);
        }

        return propertyConfiguration;
    }
}