using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;
using Blater.Frontend.Client.Auto.AutoModels.Validator;

namespace Blater.Frontend.Client.Auto.Interfaces.Validator;

public interface IAutoValidatorConfiguration<T>
{
    AutoValidatorBuilder<T> Configuration { get; set; }
    void Configure(AutoValidatorBuilder<T> builder);
}