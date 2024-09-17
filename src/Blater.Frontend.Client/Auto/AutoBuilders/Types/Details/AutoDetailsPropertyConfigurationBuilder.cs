using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public class AutoDetailsPropertyConfigurationBuilder<TModel>(AutoDetailsGroupConfiguration configuration) : IAutoDetailsMemberConfigurationBuilder<TModel>
{
    public IAutoDetailsPropertyConfiguration AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, IAutoDetailsPropertyConfiguration propertyConfiguration)
    {
        var modelType = typeof(TModel);
        var propType = typeof(TPropertyType);
        var propertyInfo = modelType.GetProperty(propType.Name);
        if (propertyInfo == null)
        {
            throw new Exception($"Property {propType.Name} not found in {modelType.Name}");
        }

        var index = configuration.Components.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            configuration.Components[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForType();
            configuration.Components.Add(propertyConfiguration);
        }

        return propertyConfiguration;
    }
}