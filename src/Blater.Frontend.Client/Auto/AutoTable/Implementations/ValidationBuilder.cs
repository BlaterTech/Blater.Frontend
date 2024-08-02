﻿using System.ComponentModel.DataAnnotations;
using Blater.Frontend.Client.Auto.AutoTable.Interfaces;
using Blater.Frontend.Client.Auto.AutoTable.Models;

namespace Blater.Frontend.Client.Auto.AutoTable.Implementations;

public class ValidationBuilder<TProperty>(ValidationConfiguration<TProperty> configuration)
    : IValidationBuilder<TProperty>
{
    public IValidationBuilder<TProperty> AddValidation(ValidationAttribute attribute)
    {
        configuration.ValidationAttributes.Add(attribute);
        return this;
    }

    public IValidationBuilder<TProperty> AddValidation(Func<TProperty, bool> rule, string errorMessage)
    {
        configuration.Rules.Add(rule);
        configuration.ErrorMessages.Add(errorMessage);
        return this;
    }
}