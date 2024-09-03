using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormAvatarConfigurationBuilder(AutoAvatarModelConfiguration modelConfiguration)
{
    public AutoFormAvatarConfigurationBuilder ContainerPublic(bool value = true)
    {
        modelConfiguration.ContainerPublic = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder ContainerPrefix(string value)
    {
        modelConfiguration.ContainerPrefix = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder AvatarUrl(string value)
    {
        modelConfiguration.AvatarUrl = value;
        return this;
    }


    public AutoFormAvatarConfigurationBuilder ExtraClass(string value)
    {
        modelConfiguration.ExtraClass = value;
        return this;
    }

    public AutoFormAvatarConfigurationBuilder ExtraStyle(string value)
    {
        modelConfiguration.ExtraStyle = value;
        return this;
    }
}