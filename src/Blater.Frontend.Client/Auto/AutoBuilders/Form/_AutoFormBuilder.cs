using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Client.Auto.AutoBuilders.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

[SuppressMessage("Performance", "CA1848:Usar os delegados LoggerMessage")]
[SuppressMessage("ReSharper", "AccessToModifiedClosure")]
public class AutoFormBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    [Parameter]
    public EventCallback<T> AfterUpsert { get; set; }

    [Parameter]
    public Func<T, Task<bool>>? BeforeUpsert { get; set; }

    [Parameter]
    public bool Processing { get; set; }

    public override AutoComponentDisplayType DisplayType { get; set; }
    public override bool HasLabel { get; set; } = true;


    public FormModelConfiguration FormModelConfiguration => ModelConfiguration.FormModelConfiguration;

    private static async Task Upsert()
    {
        await Task.Delay(1);
    }

    protected override void OnInitialized()
    {
        DisplayType = EditMode ? AutoComponentDisplayType.FormEdit : AutoComponentDisplayType.FormCreate;
    }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        var configuration = FormModelConfiguration;
        if (configuration.AutoAvatarConfiguration.EnableAvatarModel)
        {
            var avatarConfiguration = configuration.AutoAvatarConfiguration;
            builder
               .OpenElement("div")
               .AddAttribute("id", "form-with-avatar")
               .AddAttribute("class", $"d-flex {avatarConfiguration.ExtraClass}")
               .AddAttribute("style", avatarConfiguration.ExtraStyle)
               .AddContent(divBuilder =>
                {
                    divBuilder
                       .OpenElement("div")
                       .AddAttribute("id", "avatar-container")
                       .AddContent(avatarDivBuilder =>
                        {
                            //todo: criar AutoImageCircleInput
                            avatarDivBuilder
                               .OpenComponent<MudImage>()
                               .AddAttribute("Value", avatarConfiguration.AvatarUrl)
                               .AddAttribute("ContainerPrefix", avatarConfiguration.ContainerPrefix)
                               .AddAttribute("ContainerPublic", avatarConfiguration.ContainerPublic)
                               .AddAttribute("ValueChanged", EventCallback.Factory.Create<string>(this, value => avatarConfiguration.AvatarUrl = value))
                               .Close();
                        })
                       .Close();
                    divBuilder.OpenElement("div", BuildInputs);
                })
               .Close();
        }
        else
        {
            BuildInputs(builder);
        }

        return;

        void BuildInputs(EasyRenderTreeBuilder inputBuilder)
        {
            inputBuilder
               .OpenComponent<MudGrid>()
               .AddAttribute("Spacing", configuration.Spacing)
               .AddChildContent(gridBuilder =>
                {
                    foreach (var groupConfiguration in configuration.Configurations)
                    {
                        var propertyConfigurations = groupConfiguration.ComponentConfigurations[DisplayType | AutoComponentDisplayType.Form];
                        foreach (var propertyConfiguration in propertyConfigurations)
                        {
                            var mudItemBuilder = gridBuilder.OpenComponent<MudItem>();

                            if (propertyConfiguration.Breakpoints.Count > 0)
                            {
                                foreach (var (breakpoint, value) in propertyConfiguration.Breakpoints)
                                {
                                    mudItemBuilder.AddAttribute(breakpoint.ToString(), value);
                                }
                            }
                            else
                            {
                                mudItemBuilder.AddAttribute("md", 6);
                                mudItemBuilder.AddAttribute("xs", 12);
                            }

                            mudItemBuilder.AddChildContent(mudItemContentBuilder =>
                            {
                                CreateGenericComponent(mudItemContentBuilder, propertyConfiguration, DisplayType | AutoComponentDisplayType.Form);
                            });

                            mudItemBuilder.Close();
                        }
                    }
                })
               .Close();

            AfterCreateComponents(builder.RenderTreeBuilder);
        }
    }

    private void AfterCreateComponents(RenderTreeBuilder builder)
    {
        var configuration = FormModelConfiguration.AutoActionConfiguration;
        var seq = 0;

        //Divider
        builder.OpenComponent<MudDivider>(seq++);
        builder.AddAttribute(seq++, "DividerType", configuration.DividerType);
        builder.AddAttribute(seq++, "Class", $"my-6 {configuration.DividerExtraClass}");
        builder.CloseComponent();

        //Action buttons
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "id", "action-buttons");
        builder.AddAttribute(seq++, "class", $"d-flex justify-end gap-4 mt-4 {configuration.ActionExtraClass}");

        //Add cancel button
        builder.OpenComponent<MudButton>(seq++);
        builder.AddAttribute(seq++, "Variant", configuration.VariantCancelButton);
        builder.AddAttribute(seq++, "Color", configuration.ColorCancelButton);
        builder.AddAttribute(seq++, "Class", configuration.CancelButtonExtraClass);
        builder.AddAttribute(seq++, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, async _ => await NavigationService.GoBack()));
        builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(buttonBuilder => { buttonBuilder.AddContent(seq++, LocalizationService.GetValue("CancelButton")); }));
        builder.CloseComponent();

        //Add save or edit button
        builder.OpenComponent<MudButton>(seq++);
        builder.AddAttribute(seq++, "Variant", configuration.VariantCreateEditButton);
        builder.AddAttribute(seq++, "Color", configuration.ColorCreateEditButton);
        builder.AddAttribute(seq++, "ButtonType", configuration.TypeCreateEditButton);
        builder.AddAttribute(seq++, "Class", configuration.CreateEditButtonExtraClass);
        builder.AddAttribute(seq++, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, async _ => await Upsert()));
        builder.AddAttribute(seq++, "Disabled", Processing);
        builder.AddAttribute(seq++, "ChildContent",
                             (RenderFragment)(buttonBuilder =>
                             {
                                 if (Processing)
                                 {
                                     buttonBuilder.OpenComponent<MudProgressCircular>(seq++);
                                     buttonBuilder.AddAttribute(seq++, "Class", "ms-n1");
                                     buttonBuilder.AddAttribute(seq++, "Size", Size.Small);
                                     buttonBuilder.AddAttribute(seq++, "Indeterminate", true);
                                     buttonBuilder.CloseComponent();

                                     buttonBuilder.OpenComponent<MudText>(seq++);
                                     buttonBuilder.AddAttribute(seq++, "Class", "ms-2");
                                     buttonBuilder.AddAttribute(seq++, "ChildContent", (RenderFragment)(textBuilder => { textBuilder.AddContent(seq++, "Processing"); }));
                                     buttonBuilder.CloseComponent();
                                 }
                                 else
                                 {
                                     buttonBuilder.AddContent(seq, LocalizationService.GetValue(EditMode ? "EditButton" : "SaveButton"));
                                 }
                             }));
        builder.CloseComponent();

        builder.CloseElement();
    }
}