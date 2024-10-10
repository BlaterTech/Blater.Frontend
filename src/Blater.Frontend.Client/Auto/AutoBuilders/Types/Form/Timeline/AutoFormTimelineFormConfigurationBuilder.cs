using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineFormConfigurationBuilder<TModel>(AutoFormConfiguration<TModel> configuration) : IAutoFormTimelineFormConfigurationBuilder<TModel>
{
    public AutoFormConfiguration<TModel> AddForm(string formTitle, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action)
    {
        configuration.Title = formTitle;
        configuration.LocalizationId = $"Blater-AutoFormTimeline-{typeof(TModel).Name}-Form";

        var builder = new AutoFormTimelineGroupConfigurationBuilder<TModel>(configuration);

        action.Invoke(builder);

        return configuration;
    }
}