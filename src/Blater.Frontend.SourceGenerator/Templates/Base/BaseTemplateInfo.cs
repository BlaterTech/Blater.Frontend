namespace Blater.Frontend.SourceGenerator.Templates.Base;

public static class BaseTemplateInfo
{
    public const string UsingStatements = """
                                          using Blater.Frontend.Client.Auto;
                                          using Blater.Frontend.Client.Auto.AutoAttributes;
                                          using Blater.Frontend.Client.Auto.AutoBuilders;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;
                                          using Blater.Frontend.Client.Auto.AutoBuilders.Types.Details.Tabs;
                                          using Blater.Frontend.Client.Components;
                                          using Blater.Frontend.Client.Layouts;
                                          using Microsoft.AspNetCore.Components;
                                          """;
}