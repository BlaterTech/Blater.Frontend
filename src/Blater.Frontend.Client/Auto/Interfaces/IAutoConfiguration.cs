using Blater.Frontend.Client.Auto.AutoBuilders;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoConfiguration<TModel>
{
    void Configure(AutoModelConfigurationBuilder<TModel> builder);
}