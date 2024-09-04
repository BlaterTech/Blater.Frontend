﻿namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class AutoTableModelConfiguration<T>
{
    public string Title { get; set; } = string.Empty;
    public bool EnableFixedHeader { get; set; }
    public bool EnableFixedFooter { get; set; }
    public List<AutoTableAutoComponentConfiguration> Configurations { get; set; } = [];
}