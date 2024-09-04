﻿using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class AutoFormConfiguration
{
    public string Title { get; set; } = string.Empty;
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoAvatarModelConfiguration> AvatarConfiguration { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFormActionConfiguration> ActionConfiguration { get; set; } = [];
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration>> GroupConfigurations { get; set; } = [];
}