using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsGroupConfigurationBuilder(Type type, DetailsModelConfiguration configuration)
{
    public AutoDetailsGroupConfigurationBuilder AddGroup(string? title, bool disableEditButton, Action<AutoDetailsMemberConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new DetailsGroupConfiguration
        {
            Title = title ?? $"AutoDetails-Group-Title-{type.Name}",
            DisableEditButton = disableEditButton
        };

        configuration.Configurations.Add(currentGroupConfiguration);
        
        var autoDetailsGroupConfigurationBuilder = new AutoDetailsMemberConfigurationBuilder(type, currentGroupConfiguration);

        action(autoDetailsGroupConfigurationBuilder);
        
        return this;
    }
}