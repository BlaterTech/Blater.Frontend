using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;

public interface IAutoDetailsConfigurationBuilder<TModel>
{
    AutoDetailsGroupConfiguration<TModel> AddGroup(string title, Action<IAutoDetailsMemberConfigurationBuilder<TModel>> action);
    AutoDetailsGroupConfiguration<TModel> AddGroup(AutoDetailsGroupConfiguration<TModel> detailsGroupConfiguration, Action<IAutoDetailsMemberConfigurationBuilder<TModel>> action);
}