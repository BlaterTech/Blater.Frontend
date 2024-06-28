using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Blater.AutoModelConfigurations;
using Blater.AutoModelConfigurations.Configs;
using Blater.Enumerations.AutoModel;
using Blater.Extensions;
using Blater.Frontend.Auto.AutoInputs;
using Blater.FrontEnd.EasyRenderTree;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Color = MudBlazor.Color;

namespace Blater.FrontEnd.Auto.AutoBuilders;

[SuppressMessage("Usage", "CA2252:Esta API requer a aceitação de recursos de visualização")]
[SuppressMessage("Attributes/Parameters", "MUD0002:Unknown MudBlazor attribute/parameter")]
public class AutoFormBuilder<T> : BaseAutoComponentBuilder<T> where T : BaseDataModel
{
    private readonly Dictionary<string, List<string>> _validationErrors = new();

    private readonly Dictionary<string, bool> _valueChangedFlags = new();

    private bool _previousValidationErrorState;

    public bool ContainsValidationError => _validationErrors.All(x => x.Value.Count != 0);

    [Parameter]
    public EventCallback<bool> ValidationStateChanged { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    public override AutoComponentDisplayType DisplayType { get; set; }

    public override bool HasLabel { get; set; } = true;

    [Parameter]
    public EventCallback<T> AfterUpsert { get; set; }

    [Parameter]
    public Func<T, Task<bool>>? BeforeUpsert { get; set; }

    [Parameter]
    public bool NavigateGoBack { get; set; } = true;

    [Parameter]
    public bool HasValidationError { get; set; } = true;

    //todo: criar HasValidationError 
    private async Task Upsert()
    {
        if (Model == null)
        {
            Logger.LogWarning("AutoFormBuilder Model is null");
            return;
        }

        var beforeTask = BeforeUpsert?.Invoke(Model);

        if (beforeTask != null)
        {
            var result = await beforeTask;
            if (!result)
            {
                return;
            }
        }

        if (Options != null && Options?.UniqueFieldsAsStrings != null)
        {
            var modelProps = Model.GetPropertyPathsAndValues();
            foreach (var field in Options.UniqueFieldsAsStrings)
            {
                if (modelProps.ContainsKey(field))
                {
                    var modelValue = modelProps[field];
                    
                    var parameter = Expression.Parameter(Model.GetType());
                    var property = Expression.Property(parameter, field);
                    var body = Expression.Equal(property, Expression.Constant(modelValue));
                    var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

                    
                    var searchForDuplicate = await DataRepository.FindOne(lambda);
                    if (searchForDuplicate != null)
                    {
                        Snackbar.Add(Localization.Get($"{Model.GetType().Name}-SnackBar-Duplicated"), Severity.Warning);
                        return;
                    }
                }
            }
        }

        Loading = true;

        await DataRepository.Upsert(Model.Id, Model);

        Snackbar.Add(Localization.Get($"{Model.GetType().Name}-SnackBar-{(EditMode ? "Updated" : "Created")}"), Severity.Success);

        Loading = false;

        if (NavigateGoBack)
        {
            await NavigationService.GoBack();
        }

        await AfterUpsert.InvokeAsync(Model);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        DisplayType = EditMode ? AutoComponentDisplayType.FormEdit : AutoComponentDisplayType.FormCreate;
    }

    protected override void BuildComponent(EasyRenderTreeBuilder builder)
    {
        if (Model is BaseAvatarFrontendModel avatarFrontendModel)
        {
            builder.OpenElement("div")
                   .AddAttribute("id", "form-with-avatar")
                   .AddAttribute("class", "d-flex")
                   .AddContent(divBuilder =>
                    {
                        divBuilder.OpenElement("div")
                                  .AddAttribute("id", "avatar-container")
                                  .AddContent(avatarDivBuilder =>
                                   {
                                       avatarDivBuilder.OpenComponent<AutoImageCircleInput>()
                                                       .AddAttribute("Value", avatarFrontendModel.AvatarUrl)
                                                       .AddAttribute("ContainerPrefix", "avatar")
                                                       .AddAttribute("ContainerPublic", true)
                                                       .AddAttribute("ValueChanged", EventCallback.Factory.Create<string>(this, value => avatarFrontendModel.AvatarUrl = value))
                                                       .Close();
                                   }).Close();
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
            inputBuilder.OpenComponent<MudGrid>()
                        .AddAttribute("Spacing", 2)
                        .AddChildContent(gridBuilder =>
                         {
                             foreach (var componentConfiguration in ComponentConfigurations)
                             {
                                 var gridConfiguration = componentConfiguration.Grids[DisplayType];
                                 var mudItemBuilder = gridBuilder.OpenComponent<MudItem>();

                                 switch (gridConfiguration.GridType)
                                 {
                                     case AutoGridType.Normal:
                                         mudItemBuilder.AddAttribute("xs", 12);
                                         mudItemBuilder.AddAttribute("md", 6);
                                         break;
                                     case AutoGridType.FullWidth:
                                         mudItemBuilder.AddAttribute("xs", 12);
                                         break;
                                     case AutoGridType.FullHeight:
                                         break;
                                     default:
                                         throw new ArgumentOutOfRangeException();
                                 }

                                 mudItemBuilder.AddChildContent(mudItemContentBuilder => { CreateGenericComponent(mudItemContentBuilder, componentConfiguration); });

                                 mudItemBuilder.Close();
                             }
                         })
                        .Close();

            AfterCreateComponents(builder.RenderTreeBuilder);
        }
    }

    private void AfterCreateComponents(RenderTreeBuilder builder)
    {
        //Divider
        builder.OpenComponent<MudDivider>(1);
        builder.AddAttribute(2, "DividerType", DividerType.FullWidth);
        builder.AddAttribute(3, "Class", "my-6");
        builder.CloseComponent();

        //Action buttons
        builder.OpenElement(4, "div");
        builder.AddAttribute(5, "id", "action-buttons");
        builder.AddAttribute(6, "class", "d-flex justify-end gap-4 mt-4");

        //Add cancel button
        try
        {
            builder.OpenComponent<MudButton>(7);
            builder.AddAttribute(8, "Variant", Variant.Filled);
            builder.AddAttribute(9, "Color", Color.Secondary);

            builder.AddAttribute(10, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, async _ => await NavigationService.GoBack()));

            //Add ChildContent
            builder.AddAttribute(11, "ChildContent", (RenderFragment)(buttonBuilder => { buttonBuilder.AddContent(12, Localization.Get("CancelButton")); }));
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to build cancel button");
        }
        finally
        {
            builder.CloseComponent();
        }

        //Add save or edit button
        try
        {
            builder.OpenComponent<MudButton>(9);
            builder.AddAttribute(14, "Loading", false);
            builder.AddAttribute(15, "Variant", Variant.Filled);
            builder.AddAttribute(9, "Color", Color.Primary);
            builder.AddAttribute(17, "ButtonType", ButtonType.Submit);
            builder.AddAttribute(18, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, async _ => await Upsert()));

            //Add ChildContent
            builder.AddAttribute(19, "ChildContent",
                                 (RenderFragment)(buttonBuilder => { buttonBuilder.AddContent(20, EditMode ? Localization.Get("EditButton") : Localization.Get("SaveButton")); }));
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Failed to build save/edit button");
        }
        finally
        {
            builder.CloseComponent();
        }

        builder.CloseElement();
    }

    #region Validation

    public string CheckForValidationErrors(object model)
    {
        //TODO: criar dict com props e errors 
        var errorMessages = new StringBuilder();
        
        var fieldsAndErrors = new Dictionary<string, string>();

        var fieldConfigurations = ModelConfigurations.AutoConfigurations.GetValueOrDefault(model.GetType())?.ComponentConfigurations;

        if (fieldConfigurations == null)
        {
            return errorMessages.ToString();
        }

        foreach (var fieldConfiguration in fieldConfigurations)
        {
            var propertyInfo = fieldConfiguration.Property;

            if (propertyInfo == null)
            {
                continue;
            }

            var validationAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
            var propertyValue = propertyInfo.GetValue(model) ?? propertyInfo.PropertyType.GetDefaultValue();
            var localizedName = Localization.GetOrDefault(propertyInfo.Name);

            foreach (var validationAttribute in validationAttributes)
            {
                var validationContext = new ValidationContext(model)
                {
                    MemberName = localizedName
                };
                var validationResult = validationAttribute.GetValidationResult(propertyValue, validationContext);

                if (validationResult != null && validationResult != ValidationResult.Success)
                {
                    errorMessages.AppendLine($"{propertyInfo.Name}: {validationResult.ErrorMessage}");
                }
            }
        }

        return errorMessages.ToString();
    }

    private async Task InvokeValidationStateChanged()
    {
        await ValidationStateChanged.InvokeAsync(ContainsValidationError);
    }


    /// <summary>
    ///     Handlers the validation for the field
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="componentConfiguration"></param>
    /// <param name="model"></param>
    protected override void AddExtraAttributes(EasyRenderTreeBuilder builder, AutoComponentConfiguration componentConfiguration)
    {
        var propertyInfo = componentConfiguration.Property;

        if (propertyInfo == null)
        {
            Logger.LogWarning("PropertyInfo is null for {componentConfiguration}", componentConfiguration);
            return;
        }

        /*if (!_valueChangedFlags.GetValueOrDefault(propertyInfo.Name))
        {
            return;
        }*/

        var propertyValue = propertyInfo.GetValue(Model);

        if (propertyInfo.GetCustomAttribute<RequiredAttribute>() != null)
        {
            builder.RenderTreeBuilder.AddAttribute(1, "Required", true);

            //We do no neet to validate if the property is null and is required
            /*if (propertyValue == null)
            {
                return;
            }*/
        }

        // Validation Attributes
        var validationAttributes = propertyInfo.GetCustomAttributes<ValidationAttribute>();
        var errorMessages = new List<string>();

        var localizedName = Localization.GetOrDefault(propertyInfo.Name);

        // Validate property value using validation attributes
        foreach (var validationAttribute in validationAttributes)
        {
            var validationContext = new ValidationContext(Model!)
            {
                MemberName = localizedName
            };
            var validationResult = validationAttribute.GetValidationResult(propertyValue, validationContext);
            //Console.WriteLine(validationResult);
            if (validationResult == null || validationResult == ValidationResult.Success)
            {
                continue;
            }

            if (validationResult.ErrorMessage != null)
            {
                //errorMessages.Add(validationResult.ErrorMessage);
                errorMessages.Add(@Localization.Get($"{validationAttribute.GetType().Name.Replace("Attribute", "")}-{Model?.GetType()}-{propertyInfo.Name}"));
            }
        }

        _validationErrors[propertyInfo.Name] = errorMessages;

        //Console.WriteLine($"ErrorMessages: {errorMessages.Count}");
        //Console.WriteLine($"ContainsValidationError: {ContainsValidationError}");

        // Check if the validation error state has changed
        var newValidationErrorState = ContainsValidationError;
        if (_previousValidationErrorState != newValidationErrorState)
        {
            _previousValidationErrorState = newValidationErrorState;
            InvokeValidationStateChanged().ConfigureAwait(false);
        }

        // Pass error messages to the component
        var errorMessagesString = errorMessages.Count > 0 ? string.Join('\n', errorMessages) : null;
        builder.RenderTreeBuilder.AddAttribute(2, "ValidationErrorMessage", errorMessagesString);
    }
    
    protected override void FieldValueChanged(PropertyInfo propertyInfo, object? newValue)
    {
        CheckForValidationErrors(Model!);
    }
    
    #endregion
}