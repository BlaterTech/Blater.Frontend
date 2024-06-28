namespace Blater.Frontend.SourceGenerator.Templates;

public static class DetailsPageTemplate
{
    public static string GetCode(string typeName, string nameSpace, IEnumerable<string> namespaces)
    {
        var code =
            $$"""
              {{CommonTemplateInfo.UsingStatements}}

              {{string.Join("\n", namespaces.Select(x => $"using {x};"))}}

              namespace {{nameSpace}}.GeneratedPages;

              [AutoIgnore]
              [Layout(typeof(ContainerLayout))]
              [Route("/{{typeName}}/Details/{Id:guid}")]
              [SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
              public partial class {{typeName}}DetailsPage : ComponentBase
              {
                  protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
                  {
                      builder.OpenComponent <AutoDetails <{{typeName}}>>(1);
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