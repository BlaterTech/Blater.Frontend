﻿using Blater.Frontend.Client.Auto.AutoModels.Base;

namespace Blater.Frontend.Client.Auto.AutoModels.Table;

public class TableComponentConfiguration : BaseComponentConfiguration
{
    public bool DisableColumn { get; set; }
    public bool DisableFilter { get; set; }
    public bool DisableSortBy { get; set; }
}