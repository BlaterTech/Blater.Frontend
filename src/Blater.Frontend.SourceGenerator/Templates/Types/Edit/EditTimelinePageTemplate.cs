﻿using Blater.Frontend.SourceGenerator.Templates.Base;

namespace Blater.Frontend.SourceGenerator.Templates.Types.Edit;

public static class EditTimelinePageTemplate
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
              [Route("/{{typeName}}/EditTimeline/{Id:guid}")]
              
              public partial class {{typeName}}EditTimelinePage : ComponentBase
              {
                  protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
                  {
                      builder.OpenComponent<AutoFormTimelineBuilder<{{typeName}}, {{typeName}}Validation>>(1);
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