namespace Blater.Frontend.SourceGenerator.Templates;

public static class CommonTemplateInfo
{
    public const string UsingStatements = """
                                          using Blater.Attributes.Auto;
                                          using Blater.Frontend.Auto;
                                          using Blater.Frontend.Auto.AutoBuilders;
                                          using Blater.Frontend.Components;
                                          using Blater.Frontend.Layouts;
                                          using Microsoft.AspNetCore.Components;
                                          """;
}