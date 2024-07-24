﻿using System.ComponentModel.DataAnnotations;

namespace Blater.Frontend.Auto.AutoTable.Interfaces;

public interface IValidationBuilder<out TProperty>
{
    IValidationBuilder<TProperty> AddValidation(ValidationAttribute attribute);
    IValidationBuilder<TProperty> AddRule(Func<TProperty, bool> rule, string errorMessage);
}