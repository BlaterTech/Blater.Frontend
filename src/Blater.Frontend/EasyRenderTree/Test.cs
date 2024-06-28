using MudBlazor;

namespace Blater.FrontEnd.EasyRenderTree;

public class Test
{
    public void BuildComponent(EasyRenderTreeBuilder builder)
    {
        builder.OpenComponent<MudCard>()
               .AddAttribute("", "")
               .AddAttribute("", "")
               .AddAttribute("", "")
               .AddAttribute("", "")
               .AddChildContent(treeBuilder =>
                {
                    builder.OpenElement("div")
                           .AddContent(eleBuilder =>
                            {
                                eleBuilder.OpenElement("div")
                                          .AddContent(divBuilder =>
                                           {
                                               divBuilder.OpenElement("div")
                                                         .AddContent("Hello World")
                                                         .Close();
                                           })
                                          .Close();
                            })
                           .Close();
                })
               .Close();
    }
}