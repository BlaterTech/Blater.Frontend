﻿using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormGroupConfiguration<TModel>
{
    public AutoFormGroupConfiguration(string title)
    {
        Title = title;
    }
    
    public string Title { get; }
    
    public List<AutoGridConfiguration<TModel>> GridConfigurations { get; set; } = [];
    public List<IAutoFormPropertyConfiguration<TModel>> ComponentConfigurations { get; } = [];
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration<TModel>>> SubGroups { get; } = [];
}