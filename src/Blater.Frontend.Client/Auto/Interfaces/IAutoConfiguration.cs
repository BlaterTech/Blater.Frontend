using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoConfiguration
{
    void Configure(AutoComponentConfigurationBuilder builder);
}