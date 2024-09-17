using Blater.Frontend.Client.Auto.AutoBuilders.Types.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Types.Validator;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;

public interface IAutoValidatorConfiguration<T>
{
    AutoValidatorConfiguration<T> ValidatorConfiguration { get; }
    void ConfigureValidations(AutoValidatorConfigurationBuilder<T> configurationBuilder);
}