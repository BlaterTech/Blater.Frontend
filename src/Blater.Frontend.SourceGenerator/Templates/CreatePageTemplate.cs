namespace Blater.Frontend.SourceGenerator.Templates;

public static class CreatePageTemplate
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
              [Route("/{{typeName}}/Create")]
              public partial class {{typeName}}CreatePage : ComponentBase
              {
                  protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
                  {
                      builder.OpenComponent<AutoForm<{{typeName}}>>(0);
                      builder.CloseComponent();
                  }
              }
              """;

        return code;
    }
}