using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Frontend.Auto.AutoTable.Models;

namespace Blater.Frontend.Auto.AutoTable.Implementations;

public class ColumnConfigurationBuilder(ColumnConfiguration configuration) : IColumnConfigurationBuilder
{
    public IColumnConfigurationBuilder HasColumnName(string columnName)
    {
        configuration.HasColumnName = columnName;
        return this;
    }

    public IColumnConfigurationBuilder MaxLength(int value)
    {
        configuration.MaxLength = value;
        return this;
    }

    public IColumnConfigurationBuilder DataFormat(string format)
    {
        configuration.DataFormat = format;
        return this;
    }

    public IColumnConfigurationBuilder Class(string cssClass)
    {
        configuration.Class = cssClass;
        return this;
    }

    public IColumnConfigurationBuilder Style(string style)
    {
        configuration.Style = style;
        return this;
    }

    public IColumnConfigurationBuilder Order(int order)
    {
        configuration.Order = order;
        return this;
    }

    public IColumnConfigurationBuilder MergeColumn(int merge)
    {
        configuration.MergeColumn = merge;
        return this;
    }

    public IColumnConfigurationBuilder IsRequired()
    {
        configuration.IsRequired = true;
        return this;
    }
}