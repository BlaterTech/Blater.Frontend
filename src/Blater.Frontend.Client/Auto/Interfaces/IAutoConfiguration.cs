using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoConfiguration<T> where T : BaseDataModel
{
    void Configure(AutoTableConfigurationBuilder<T> builder)
    {
        
    }

    void Configure(AutoFormConfigurationBuilder<T> builder)
    {
        
    }
}