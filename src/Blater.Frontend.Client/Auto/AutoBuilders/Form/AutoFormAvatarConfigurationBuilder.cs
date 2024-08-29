using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormAvatarConfigurationBuilder(AutoAvatarConfiguration configuration)
{
    public AutoFormAvatarConfigurationBuilder ContainerPublic(bool value = true)
    {
        configuration.ContainerPublic = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder ContainerPrefix(string value)
    {
        configuration.ContainerPrefix = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder AvatarUrl(string value)
    {
        configuration.AvatarUrl = value;
        return this;
    }


    public AutoFormAvatarConfigurationBuilder ExtraClass(string value)
    {
        configuration.ExtraClass = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder ExtraStyle(string value)
    {
        configuration.ExtraStyle = value;
        return this;
    }
}