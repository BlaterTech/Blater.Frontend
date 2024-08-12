using Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

public interface IAutoTableConfiguration<TTable> where TTable : BaseDataModel
{
    void Configure(AutoTableConfigurationBuilder<TTable> builder);
}