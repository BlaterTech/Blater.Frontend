namespace Blater.Frontend.Auto.AutoTable.Interfaces;

public interface IColumnConfigurationBuilder
{
    IColumnConfigurationBuilder HasColumnName(string columnName);
    IColumnConfigurationBuilder MaxLength(int value);
    IColumnConfigurationBuilder DataFormat(string format);
    IColumnConfigurationBuilder Class(string cssClass);
    IColumnConfigurationBuilder Style(string style);
    IColumnConfigurationBuilder Order(int order);
    IColumnConfigurationBuilder MergeColumn(int merge);
    IColumnConfigurationBuilder IsRequired();
}