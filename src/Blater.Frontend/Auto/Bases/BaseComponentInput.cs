﻿using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.Bases;

public class BaseComponentInput :
    ComponentBase
{
    [Parameter]
    public bool HasValidationError { get; set; }

    [Parameter]
    public string? ValidationErrorMessage { get; set; }

    [Parameter]
    public bool Required { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public string? Label { get; set; }
}