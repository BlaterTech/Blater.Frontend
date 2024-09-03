using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormAvatarConfigurationBuilder(AutoAvatarModelConfiguration modelConfiguration) : IAutoFormAvatarConfigurationBuilder
{
    public IAutoFormAvatarConfigurationBuilder ContainerPublic(bool value = true)
    {
        modelConfiguration.ContainerPublic = value;
        return this;
    }

    public IAutoFormAvatarConfigurationBuilder ContainerPrefix(string value)
    {
        modelConfiguration.ContainerPrefix = value;
        return this;
    }

    public IAutoFormAvatarConfigurationBuilder AvatarUrl(string value)
    {
        modelConfiguration.AvatarUrl = value;
        return this;
    }
    
    public IAutoFormAvatarConfigurationBuilder ExtraClass(string value)
    {
        modelConfiguration.ExtraClass = value;
        return this;
    }

    public IAutoFormAvatarConfigurationBuilder ExtraStyle(string value)
    {
        modelConfiguration.ExtraStyle = value;
        return this;
    }
}