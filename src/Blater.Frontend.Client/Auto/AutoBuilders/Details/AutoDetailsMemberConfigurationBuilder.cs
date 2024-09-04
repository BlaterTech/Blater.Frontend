﻿using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, AutoDetailsGroupConfiguration configuration)
{
    public AutoDetailsMemberConfigurationBuilder AddMember<TType>(Expression<Func<TType>> expression, AutoDetailsAutoComponentConfiguration componentConfiguration)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var index = configuration.Configurations.IndexOf(componentConfiguration);
        if (index != -1)
        {
            configuration.Configurations[index] = componentConfiguration;
            return this;
        }

        componentConfiguration.Property = property;
        componentConfiguration.AutoComponentType ??= property.GetDefaultAutoDetailsComponentForType();
        configuration.Configurations.Add(componentConfiguration);
        return this;
    }
}