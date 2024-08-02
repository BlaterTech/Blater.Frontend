using Blater.Frontend.Client.Auto.AutoTable.Implementations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoTable.Interfaces;

public interface ITableConfiguration<TTable> where TTable : BaseDataModel
{
    void Configure(TableConfigurationBuilder<TTable> builder);
}