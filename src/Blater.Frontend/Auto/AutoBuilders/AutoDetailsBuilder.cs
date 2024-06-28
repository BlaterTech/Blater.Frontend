using System.Diagnostics.CodeAnalysis;
using Blater.Enumerations.AutoModel;
using Blater.Frontend.Auto;
using Blater.FrontEnd.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.FrontEnd.Auto.AutoBuilders;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
public class AutoDetailsBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    public override AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Details;

    public override bool HasLabel { get; set; }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        //Auto Avatar Details
        if (Model is BaseAvatarFrontendModel avatarFrontendModel)
        {
            builder.OpenComponent<AutoDetailsAvatar>()
                   .AddAttribute("Title", avatarFrontendModel.Title)
                   .AddAttribute("SubTitle", avatarFrontendModel.SubTitle)
                   .AddAttribute("AvatarUrl", avatarFrontendModel.AvatarUrl)
                   .Close();
        }

        var modelType = typeof(T);

        //Start MudCard
        builder.OpenComponent<MudCard>()
               //.AddAttribute("Elevation", 0)
               .AddAttribute("Class", "my-4 pa-4")
               .AddChildContent(cardBuilder =>
                {
                    cardBuilder.OpenElement("div")
                               .AddAttribute("class", "d-flex justify-space-between my-2")
                               .AddContent(divBuilder =>
                                {
                                    var groupLabelName = Localization.Get($"{DisplayType.ToString()}-GroupLabel-{modelType.Name}");
                                    divBuilder.OpenComponent<MudText>()
                                              .AddAttribute("LabelName", groupLabelName)
                                              .AddAttribute("Typo", Typo.h6)
                                              .AddChildContent(textBuilder => { textBuilder.AddContent(groupLabelName); })
                                              .Close();

                                    if (HasEditButton)
                                    {
                                        divBuilder.OpenComponent<MudIconButton>()
                                                  .AddAttribute("Variant", Variant.Filled)
                                                  .AddAttribute("Color", Color.Primary)
                                                  .AddAttribute("OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, async () => await EditCallback.InvokeAsync(Model)))
                                                  .AddAttribute("Icon", Icons.Material.Outlined.Edit)
                                                  .Close();
                                    }
                                })
                               .Close();


                    cardBuilder.OpenComponent<MudSimpleTable>()
                               .AddAttribute("Style", "overflow-x: auto;")
                               .AddAttribute("Hover", true)
                               .AddAttribute("Elevation", 0)
                               .AddAttribute("Striped", true)
                               .AddChildContent(tableBuilder =>
                                {
                                    tableBuilder.OpenElement("thead")
                                                .Close();
                                    tableBuilder.OpenElement("tbody")
                                                .AddContent(tbodyBuilder =>
                                                 {
                                                     foreach (var componentConfiguration in ComponentConfigurations.Where(
                                                                  x => x is { SeparateCard: false, SeparateComponent: false }))
                                                     {
                                                         if (componentConfiguration.Property?.Name != null 
                                                          && Options != null 
                                                          && Options.IgnoreFieldsAsStrings.Contains(componentConfiguration.Property.Name))
                                                         {
                                                             continue;
                                                         }
                                                         tbodyBuilder.OpenElement("tr")
                                                                     .AddContent(trBuilder =>
                                                                      {
                                                                          if (componentConfiguration.ComponentTypes[DisplayType] != AutoComponentType.AutoTitle)
                                                                          {
                                                                              trBuilder.OpenElement("td")
                                                                                       .AddContent(tdBuilder =>
                                                                                        {
                                                                                            tdBuilder.AddContent(
                                                                                                Localization.Get(
                                                                                                    $"Details-{modelType.Name}-{componentConfiguration.Property?.Name}"));
                                                                                        })
                                                                                       .Close();
                                                                              if (!string.IsNullOrEmpty(ExtraStyle))
                                                                              {
                                                                                  trBuilder.OpenElement("td")
                                                                                           .AddAttribute("style", ExtraStyle)
                                                                                           .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                                                                           .Close();
                                                                              }
                                                                              else
                                                                              {
                                                                                  trBuilder.OpenElement("td")
                                                                                           .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                                                                           .Close();
                                                                              }
                                                                          }
                                                                          else
                                                                          {
                                                                              trBuilder.OpenElement("td")
                                                                                       .AddContent(tdBuilder => { CreateGenericComponent(tdBuilder, componentConfiguration); })
                                                                                       .Close();
                                                                              trBuilder.OpenElement("td")
                                                                                       .Close();
                                                                          }
                                                                      })
                                                                     .Close();
                                                     }
                                                 })
                                                .Close();
                                })
                               .Close();
                })
               .Close();

        foreach (var componentConfiguration in ComponentConfigurations.Where(x => x.SeparateCard))
        {
            builder.OpenComponent<MudCard>()
                   .AddAttribute("Class", "my-4 pa-4")
                   .AddChildContent(cardBuilder => { CreateGenericComponent(cardBuilder, componentConfiguration); })
                   .Close();
        }


        foreach (var componentConfiguration in ComponentConfigurations.Where(x => x.SeparateComponent))
        {
            builder.OpenElement("div")
                   .AddAttribute("class", "my-4")
                   .AddContent(divBuilder => { CreateGenericComponent(divBuilder, componentConfiguration); })
                   .Close();
        }
    }
}