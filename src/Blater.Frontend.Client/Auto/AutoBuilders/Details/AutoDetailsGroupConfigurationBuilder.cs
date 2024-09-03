using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsGroupConfigurationBuilder<TModel>(AutoDetailsModelConfiguration<TModel> configuration)
{
    public AutoDetailsGroupConfigurationBuilder<TModel> AddGroup(string? title, bool disableEditButton, Action<AutoDetailsMemberConfigurationBuilder> action)
    {
        var type = typeof(TModel);
        var currentGroupConfiguration = new AutoDetailsGroupConfiguration
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