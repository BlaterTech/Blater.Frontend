using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

public class AutoTableEventConfigurationBuilder<TPropertyType>(AutoTablePropertyConfiguration<TPropertyType> configuration) : IAutoTableEventConfigurationBuilder<TPropertyType>
{
    public AutoTablePropertyConfiguration<TPropertyType> AddOnValueChanged(Action<TPropertyType> onValueChanged)
    {
        configuration.OnValueChanged = EventCallback.Factory.Create(this, onValueChanged);
        return configuration;
    }

    public AutoTablePropertyConfiguration<TPropertyType> AddOnClick(EventCallback<EventArgs> onClick)
    {
        configuration.OnClick = onClick;
        return configuration;
    }
}