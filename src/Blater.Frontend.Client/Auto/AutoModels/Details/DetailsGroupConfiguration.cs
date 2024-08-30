﻿namespace Blater.Frontend.Client.Auto.AutoModels.Details;

public class DetailsGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public bool DisableEditButton { get; set; }
    public List<DetailsComponentConfiguration> Configurations { get; set; } = [];
}