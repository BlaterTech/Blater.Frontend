﻿using Blater.Frontend.Auto.AutoTable.Implementations;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.AutoTable.Interfaces;

public interface IColumnConfigurationBuilder<T> where T : BaseDataModel
{
    IColumnConfigurationBuilder<T> HasColumnName(string columnName);
    IColumnConfigurationBuilder<T> MaxLength(int value);
    IColumnConfigurationBuilder<T> DataFormat(string format);
    IColumnConfigurationBuilder<T> Class(string cssClass);
    IColumnConfigurationBuilder<T> Style(string style);
    IColumnConfigurationBuilder<T> Order(int order);   
    IColumnConfigurationBuilder<T> MergeColumn(int merge);
    IColumnConfigurationBuilder<T> IsRequired();
    IColumnConfigurationBuilder<T> HasValidation(Action<IValidationBuilder<T>> configure);
    IColumnConfigurationBuilder<T> HasValidation(Func<T, bool> validationRule, string errorMessage);
    IColumnConfigurationBuilder<T> ComponentType(string style);
    IColumnConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> callback);
}   