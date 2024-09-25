using Blater.Frontend.SourceGenerator.Templates.Base;

namespace Blater.Frontend.SourceGenerator.Templates.Types.Edit;

public static class EditTimelinePageTemplate
{
    public static string GetCode(string typeName, string nameSpace, IEnumerable<string> namespaces)
    {
        var code =
            $$"""
              {{BaseTemplateInfo.UsingStatements}}

              {{string.Join("\n", namespaces.Select(x => $"using {x};"))}}

              namespace {{nameSpace}}.GeneratedPages;

              [AutoIgnore]
              [Layout(typeof(ContainerLayout))]
              [Route("/{{typeName}}/EditTimeline/{Id:guid}")]
              
              public partial class {{typeName}}EditPage : ComponentBase
              {
                  protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
                  {
                      builder.OpenComponent<AutoFormTimelineBuilder<{{typeName}}>>(1);
                      builder.AddAttribute(2, "Id", Id);
                      builder.CloseComponent();
                  }
                  
                  [Parameter]
                  public Guid Id { get; set; }
              }
              """;

        return code;
    }
}