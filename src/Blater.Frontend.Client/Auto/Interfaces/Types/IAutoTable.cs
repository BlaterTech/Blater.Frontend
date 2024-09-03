using Blater.Frontend.Client.Auto.AutoBuilders;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;

namespace Blater.Frontend.Client.Auto.Interfaces.Types;

public interface IAutoTable<T>
{
    void ConfigureTable(AutoTableMemberConfigurationBuilder builder);
}