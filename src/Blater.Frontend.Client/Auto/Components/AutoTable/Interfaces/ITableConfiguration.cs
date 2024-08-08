using Blater.Frontend.Client.Auto.Components.AutoTable.Builders;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;

public interface ITableConfiguration<TTable> where TTable : BaseDataModel
{
    void Configure(TableConfigurationBuilder<TTable> builder);
}