using Blater.Frontend.Client.Auto.AutoBuilders;

namespace Blater.Frontend.Client.Auto.Interfaces.Types;

public interface IAutoForm<T>
{
    void ConfigureForm(AutoModelConfigurationBuilder<T> builder);
}