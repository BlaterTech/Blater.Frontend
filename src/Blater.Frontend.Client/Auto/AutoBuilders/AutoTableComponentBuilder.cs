using Blater.Frontend.Client.Auto.Components.AutoTable.Models;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoTableComponentBuilder<T> : BaseAutoComponentBuilder<T, TableConfiguration<T>> where T : BaseDataModel
{
    protected override void CreateGenericComponent(EasyRenderTreeBuilder builder, TableConfiguration<T> componentConfiguration)
    {
        foreach (var columnConfiguration in componentConfiguration.Columns.Where(x => x.EnabledColumn))
        {
            var propertyInfo = columnConfiguration.PropertyInfo;
            var componentBuilderType = propertyInfo.GetType();
            
            var componentRenderBuilder = builder.OpenComponent(componentBuilderType);

            componentRenderBuilder.AddAttribute("ComponentConfiguration", componentConfiguration);
        }
    }
}