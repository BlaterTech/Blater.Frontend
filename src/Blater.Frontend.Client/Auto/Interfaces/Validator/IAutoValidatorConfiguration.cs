using Blater.Frontend.Client.Auto.AutoBuilders.Valitador;

namespace Blater.Frontend.Client.Auto.Interfaces.Validator;

public interface IAutoValidatorConfiguration<T>
{
    void ConfigureValidator(AutoValidatorBuilder<T> builder);
}