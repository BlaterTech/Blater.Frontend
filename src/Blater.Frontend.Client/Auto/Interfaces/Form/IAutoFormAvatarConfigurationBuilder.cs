using Blater.Frontend.Client.Auto.AutoBuilders.Form;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormAvatarConfigurationBuilder
{
    
    IAutoFormAvatarConfigurationBuilder ContainerPublic(bool value = true);
    IAutoFormAvatarConfigurationBuilder ContainerPrefix(string value);
    IAutoFormAvatarConfigurationBuilder AvatarUrl(string value);
    IAutoFormAvatarConfigurationBuilder ExtraClass(string value);
    IAutoFormAvatarConfigurationBuilder ExtraStyle(string value);
}