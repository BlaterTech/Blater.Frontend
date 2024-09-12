using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Validator;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;

public interface IAutoValidatorConfiguration<T>
{
    AutoValidatorConfiguration<T> ValidatorConfiguration { get; }
    void Configure(AutoValidatorConfigurationBuilder<T> configurationBuilder);
}