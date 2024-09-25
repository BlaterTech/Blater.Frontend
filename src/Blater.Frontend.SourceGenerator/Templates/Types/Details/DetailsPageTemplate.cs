using Blater.Frontend.SourceGenerator.Templates.Base;

namespace Blater.Frontend.SourceGenerator.Templates.Types.Details;

public static class DetailsPageTemplate
{
    public static string GetCode(string typeName, string nameSpace, IEnumerable<string> namespaces, string layoutPreference)
    {
        var code =
            $$"""
              {{BaseTemplateInfo.UsingStatements}}

              {{string.Join("\n", namespaces.Select(x => $"using {x};"))}}

              namespace {{nameSpace}}.GeneratedPages;

              [AutoIgnore]
              [Layout(typeof({{layoutPreference}}))]
              [Route("/{{typeName}}/Details/{Id:guid}")]
              
              public partial class {{typeName}}DetailsPage : ComponentBase
              {
                  protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
                  {
                      builder.OpenComponent <AutoDetailsBuilder<{{typeName}}>>(1);
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