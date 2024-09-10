using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces.Types.Table;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Table;
    public override bool HasLabel { get; set; }
    private AutoTableConfiguration Configuration { get; set; } = default!;

    protected override void LoadModelConfig()
    {
        var autoTable = FindModelConfig<IAutoTableConfiguration>();
        Configuration = autoTable.TableConfiguration;
    }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenComponent<MudCard>()
           .AddChildContent(cardBuilder =>
            {
                BuildFilterComponent(cardBuilder);
                BuildTableComponent(cardBuilder);
            })
           .Close();
    }

    private void BuildFilterComponent(EasyRenderTreeBuilder builder)
    {
        builder
           .OpenElement("form")
           .AddChildContent(formGridBuilder =>
            {
                formGridBuilder
                   .OpenComponent<MudGrid>()
                   .AddChildContent(formGridContentBuilder =>
                    {
                        
                    })
                   .Close();
            })
           .Close();
    }

    private void BuildTableComponent(EasyRenderTreeBuilder builder)
    {
        
    }

    private void BuildHeaderContent(EasyRenderTreeBuilder builder)
    {
        
    }

    private void BuildRowContent(EasyRenderTreeBuilder builder)
    {
        
    }
}