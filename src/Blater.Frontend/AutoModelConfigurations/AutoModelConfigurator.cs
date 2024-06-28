using System.Linq.Expressions;
using System.Reflection;
using Blater.Frontend.AutoModelConfigurations.Configs;
using Blater.Frontend.AutoModelConfigurations.Interfaces;
using Blater.Frontend.Enumerations.AutoModel;

namespace Blater.Frontend.AutoModelConfigurations;

public class AutoModelConfigurator : IAutoModelConfigurator
{
    private readonly AutoModelConfiguration _modelConfiguration;
    
    public AutoModelConfigurator(Type modelType)
    {
        _modelConfiguration = new AutoModelConfiguration
        {
            ModelType = modelType,
            ModelName = modelType.Name,
            Grid = new AutoGridConfiguration
            {
                Columns = 2
            }
        };
        
        ModelConfigurations.Configurations.Add(modelType, _modelConfiguration);
    }
    
    public IAutoComponentPropertyConfigurator Property<TProperty>(Expression<Func<TProperty>> propertyExpression)
    {
        if (propertyExpression.Body is not MemberExpression { Member: PropertyInfo propertyInfo })
        {
            throw new ArgumentException("Expression must be a property expression");
        }
        
        //Check if the property is already configured
        var existingConfiguration = _modelConfiguration.ComponentConfigurations
                                                       .FirstOrDefault(x => x.Property == propertyInfo);
        if (existingConfiguration != null)
        {
            return new AutoComponentConfigurator(existingConfiguration);
        }
        
        return CreateAutoFieldConfigurator(propertyInfo.Name, propertyInfo);
    }
    
    public IAutoModelConfigurator Model => this;
    
    public IAutoModelConfigurator GridType(AutoGridType gridType)
    {
        _modelConfiguration.Grid.GridType = gridType;
        return this;
    }
    
    public IAutoModelConfigurator CanBeDisabled(bool value)
    {
        _modelConfiguration.CanBeDisabled = value;
        return this;
    }
    
    public IAutoComponentPropertyConfigurator Component(string name)
    {
        return CreateAutoFieldConfigurator(name);
    }
    
    private AutoComponentConfigurator CreateAutoFieldConfigurator(string name, PropertyInfo? propertyInfo = default)
    {
        var currentComponentConfiguration = new AutoComponentConfiguration();
        
        if (propertyInfo != null)
        {
            currentComponentConfiguration.Property = propertyInfo;
        }
        
        //Initialize dictionaries
        foreach (var showFlag in AutoComponentDisplayTypeExtensions.GetValues())
        {
            currentComponentConfiguration.Order.TryAdd(showFlag, 0);
            //currentComponentConfiguration.AutoComponentTypes.TryAdd(showFlag, BaseAutoComponentType.None);
            currentComponentConfiguration.Sizes.TryAdd(showFlag, AutoFieldSize.Normal);
            currentComponentConfiguration.Grids.TryAdd(showFlag, new AutoGridConfiguration());
        }
        
        _modelConfiguration.ComponentConfigurations.Add(currentComponentConfiguration);
        
        return new AutoComponentConfigurator(currentComponentConfiguration);
    }
}