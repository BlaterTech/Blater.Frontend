using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Auto.AutoBuilders;
using Blater.Frontend.Models;
using Blater.Frontend.Services;
using Blater.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto;


public partial class AutoForm<T> where T : BaseDataModel
{
    private readonly string _typeName = typeof(T).Name;

    [Inject] public LocalizationService LocalizationService { get; set; } = null!;

    [Parameter]
    public BlaterId? Id { get; set; }

    [Parameter]
    public T? Model { get; set; }

    [Parameter]
    public FindPropertiesAttributeOptions<T>? Options { get; set; }
    
    [Parameter]
    public Func<T, Task<bool>>? BeforeUpsert { get; set; }

    private AutoFormBuilder<T> FormBuilder { get; set; } = null!;
}