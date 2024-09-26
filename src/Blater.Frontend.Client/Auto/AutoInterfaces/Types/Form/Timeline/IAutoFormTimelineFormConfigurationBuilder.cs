using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;

public interface IAutoFormTimelineFormConfigurationBuilder<TModel>
{
    AutoFormConfiguration<TModel> AddForm(string formTitle, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action);
}