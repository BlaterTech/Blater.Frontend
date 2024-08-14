using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableComponentBuilder<T> : BaseAutoComponentBuilder<T, TableConfiguration<T>> where T : BaseDataModel
{
    protected override void CreateGenericComponent(EasyRenderTreeBuilder builder, TableConfiguration<T> componentConfiguration)
    {
        foreach (var columnConfiguration in componentConfiguration.Columns.Where(x => x.DisableColumn))
        {
            var propertyInfo = columnConfiguration.PropertyInfo;
            var componentBuilderType = propertyInfo.GetType();
            
            var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

            componentRenderBuilder.AddAttribute("ComponentConfiguration", componentConfiguration);
        }
    }
}