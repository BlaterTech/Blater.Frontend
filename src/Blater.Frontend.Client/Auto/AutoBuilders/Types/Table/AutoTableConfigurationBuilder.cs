using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;
using Blater.Frontend.Client.Contracts;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

public class AutoTableConfigurationBuilder<TModel> : IAutoTableConfigurationBuilder<TModel>
{
    private readonly AutoTableConfiguration<TModel> _configuration;

    public AutoTableConfigurationBuilder(object instance)
    {
        if (instance is IAutoTableConfiguration<TModel> configuration)
        {
            _configuration = configuration.TableConfiguration;
            _configuration.LocalizationId ??= $"Blater-AutoTable-{typeof(TModel).Name}";
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoTableConfiguration<{typeof(TModel).Name}>");
        }
    }
    
    public AutoTableEventConfigurationBuilder<TModel, TPropertyType> AddFilter<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        var propertyInfo = expression.GetPropertyInfo();
        
        var index = _configuration.CustomAutoTableFilters.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            _configuration.CustomAutoTableFilters[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.EnableDefaultValue = true;
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.LabelNameLocalizationId ??= $"Blater-AutoTable-{typeof(TModel).Name}-Filter-LabelName-{propertyInfo.Name}";
            propertyConfiguration.LabelName ??= propertyInfo.Name;
            propertyConfiguration.Placeholder ??= propertyInfo.Name;
            propertyConfiguration.PlaceHolderLocalizationId ??= $"Blater-AutoTable-{typeof(TModel).Name}-Filter-Placeholder-{propertyInfo.Name}";
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForForm();
            _configuration.CustomAutoTableFilters.Add(propertyConfiguration);
        }

        return new AutoTableEventConfigurationBuilder<TModel, TPropertyType>(propertyConfiguration);
    }
    
    public CustomAutoTableAction AddCustomAction(CustomAutoTableAction autoTableAction)
    {
        var index = _configuration.CustomAutoTableActions.IndexOf(autoTableAction);
        if (index != -1)
        {
            _configuration.CustomAutoTableActions[index] = autoTableAction;
        }
        else
        {
            _configuration.CustomAutoTableActions.Add(autoTableAction);
        }

        return autoTableAction;
    }
    
    public AutoTablePagerConfiguration ModifyTablePager(AutoTablePagerConfiguration pagerConfiguration)
    {
        _configuration.PagerConfiguration = pagerConfiguration;

        return _configuration.PagerConfiguration;
    }

    public AutoTablePropertyConfiguration<TModel, TPropertyType> AddMemberOnly<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return propertyConfiguration;
    }
    
    public IAutoTableEventConfigurationBuilder<TModel, TPropertyType> AddMemberWithEvent<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return new AutoTableEventConfigurationBuilder<TModel, TPropertyType>(propertyConfiguration);
    }
    
    private void AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        var propertyInfo = expression.GetPropertyInfo();
        
        var index = _configuration.Configurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            _configuration.Configurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.LabelNameLocalizationId ??= $"Blater-AutoTable-{typeof(TModel).Name}-Member-LabelName-{propertyInfo.Name}";
            propertyConfiguration.LabelName ??= propertyInfo.Name;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForTable();
            _configuration.Configurations.Add(propertyConfiguration);
        }
    }
}