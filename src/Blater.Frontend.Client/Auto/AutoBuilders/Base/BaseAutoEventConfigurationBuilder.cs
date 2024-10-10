using Blater.Frontend.Client.Auto.AutoInterfaces.Base;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Services;

using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Base;

public class BaseAutoEventConfigurationBuilder<TModel, TPropertyType, TResponseType>(
    BaseAutoPropertyConfiguration<TPropertyType> configuration)
    : IBaseAutoEventConfigurationBuilder<TPropertyType, TResponseType>
    where TResponseType : BaseAutoPropertyConfiguration<TPropertyType>
{
    public TResponseType AddOnValueChanged(Func<TPropertyType, TPropertyType> onValueChanged)
    {
        configuration.OnValueChanged = EventCallback.Factory.Create(this, (TPropertyType x) =>
        {
            configuration.Value = x;
            configuration.Value = onValueChanged(configuration.Value);
            AddNotifyStateHasChanged(configuration);
        });
        return (TResponseType)configuration;
    }

    public TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged, IBaseAutoPropertyConfigurationValue<TPropertyType> value)
    {
        configuration.OnValueChanged = EventCallback.Factory.Create(this, (TPropertyType x) =>
        {
            // Chama a ação passada
            onValueChanged.Invoke(x);

            // Atualiza o valor da configuração
            configuration.Value = x;

            // Atualiza o valor do membro específico, se necessário
            value.Value = x; // Aqui você pode atribuir o valor de 'x' a 'value.Value'
            AddNotifyStateHasChanged(value); // Notifica a mudança de estado para o valor

            // Notifica a mudança de estado para a configuração
            AddNotifyStateHasChanged(configuration);
        });
        return (TResponseType)configuration;
    }

    public TResponseType AddOnValueChanged(Action<TPropertyType> onValueChanged, params IBaseAutoPropertyConfigurationValue<TPropertyType>[] values)
    {
        configuration.OnValueChanged = EventCallback.Factory.Create(this, (TPropertyType x) =>
        {
            onValueChanged.Invoke(x);

            configuration.Value = x;
            AddNotifyStateHasChanged(configuration);

            foreach (var value in values)
            {
                AddNotifyStateHasChanged(value);
            }
        });
        return (TResponseType)configuration;
    }

    public TResponseType AddOnClick(Action onClick)
    {
        configuration.OnClick = EventCallback.Factory.Create<EventArgs>(this, onClick);
        return (TResponseType)configuration;
    }

    private void AddNotifyStateHasChanged(IBaseAutoPropertyConfigurationValue<TPropertyType> value)
    {
        StateNotifierService.NotifyStateHasChanged(value.Property, value.Value!);
    }
}