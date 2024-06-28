using Blater.Frontend.AutoModelConfigurations.Configs;
using Blater.Frontend.AutoModelConfigurations.Interfaces;
using Blater.Frontend.Enumerations.AutoModel;

namespace Blater.Frontend.AutoModelConfigurations;

public class AutoComponentConfigurator : IAutoComponentPropertyConfigurator
{
    public AutoComponentConfigurator(AutoComponentConfiguration currentComponentConfiguration)
    {
        CurrentComponentConfiguration = currentComponentConfiguration;
    }
    
    public AutoComponentConfiguration CurrentComponentConfiguration { get; set; }
    
    /// <summary>
    ///     Sets the order on all show flag
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public IAutoComponentPropertyConfigurator Order(int order)
    {
        foreach (var showFlag in AutoComponentDisplayTypeExtensions.GetValues())
        {
            CurrentComponentConfiguration.Order[showFlag] = order;
        }
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator Order(AutoComponentDisplayType displayType, int order)
    {
        CurrentComponentConfiguration.Order[displayType] = order;
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormOrder(int order)
    {
        Order(AutoComponentDisplayType.FormEdit, order);
        Order(AutoComponentDisplayType.FormCreate, order);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormEditOrder(int order)
    {
        return Order(AutoComponentDisplayType.FormEdit, order);
    }
    
    public IAutoComponentPropertyConfigurator FormCreateOrder(int order)
    {
        return Order(AutoComponentDisplayType.FormCreate, order);
    }
    
    public IAutoComponentPropertyConfigurator FilterOrder(int order)
    {
        return Order(AutoComponentDisplayType.Filter, order);
    }
    
    public IAutoComponentPropertyConfigurator Table(int order, AutoComponentType displayComponentType)
    {
        TableDisplayType(displayComponentType);
        Order(AutoComponentDisplayType.DataGrid, order);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator TableOrder(int order)
    {
        return Order(AutoComponentDisplayType.DataGrid, order);
    }
    
    public IAutoComponentPropertyConfigurator DetailsOrder(int order)
    {
        return Order(AutoComponentDisplayType.Details, order);
    }
    
    #region LabelName
    
    public IAutoComponentPropertyConfigurator LabelName(string labelName)
    {
        CurrentComponentConfiguration.ExtraAttributes.TryAdd("LabelName", labelName);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator HasParent(IAutoComponentPropertyConfigurator parent)
    {
        //parent.CurrentComponentConfiguration.ChildrenConfigurations.Add(CurrentComponentConfiguration);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator SeparateCard()
    {
        CurrentComponentConfiguration.SeparateCard = true;
        return this;
    }
    
    public IAutoComponentPropertyConfigurator SeparateComponent()
    {
        CurrentComponentConfiguration.SeparateComponent = true;
        return this;
    }
    
    #endregion
    
    #region Size
    
    public IAutoComponentPropertyConfigurator DetailsSize(AutoFieldSize autoFieldSize)
    {
        CurrentComponentConfiguration.Sizes[AutoComponentDisplayType.Details] = autoFieldSize;
        
        return this;
    }
    
    private IAutoComponentPropertyConfigurator FormSize(AutoComponentDisplayType show, AutoFieldSize autoFieldSize)
    {
        CurrentComponentConfiguration.Sizes[show] = autoFieldSize;
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormSize(AutoFieldSize autoFieldSize)
    {
        FormSize(AutoComponentDisplayType.FormEdit, autoFieldSize);
        FormSize(AutoComponentDisplayType.FormCreate, autoFieldSize);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormCreateSize(AutoFieldSize autoFieldSize)
    {
        return FormSize(AutoComponentDisplayType.FormCreate, autoFieldSize);
    }
    
    public IAutoComponentPropertyConfigurator FormEditSize(AutoFieldSize autoFieldSize)
    {
        return FormSize(AutoComponentDisplayType.FormEdit, autoFieldSize);
    }
    
    public IAutoComponentPropertyConfigurator TableSize(AutoFieldSize autoFieldSize)
    {
        CurrentComponentConfiguration.Sizes[AutoComponentDisplayType.DataGrid] = autoFieldSize;
        
        return this;
    }
    
    #endregion
    
    #region FormType
    
    private IAutoComponentPropertyConfigurator FormDisplayType(AutoComponentDisplayType show, AutoFormInputType formType)
    {
        CurrentComponentConfiguration.AutoComponentTypes[show] = formType;
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormDisplayType(AutoFormInputType displayType)
    {
        FormDisplayType(AutoComponentDisplayType.FormEdit, displayType);
        FormDisplayType(AutoComponentDisplayType.FormCreate, displayType);
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FormEditDisplayType(AutoFormInputType displayType)
    {
        return FormDisplayType(AutoComponentDisplayType.FormEdit, displayType);
    }
    
    public IAutoComponentPropertyConfigurator FormCreateDisplayType(AutoFormInputType displayType)
    {
        return FormDisplayType(AutoComponentDisplayType.FormCreate, displayType);
    }
    
    public IAutoComponentPropertyConfigurator FilterDisplayType(AutoFormInputType componentType)
    {
        return FormDisplayType(AutoComponentDisplayType.Filter, componentType);
    }
    
    #endregion
    
    #region DisplayType
    
    private IAutoComponentPropertyConfigurator ComponentDisplayType(AutoComponentDisplayType show, AutoComponentType componentType)
    {
        CurrentComponentConfiguration.AutoComponentTypes[show] = componentType;
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator DetailsDisplayType(AutoComponentType displayComponentType)
    {
        return ComponentDisplayType(AutoComponentDisplayType.Details, displayComponentType);
    }
    
    
    public IAutoComponentPropertyConfigurator TableDisplayType(AutoComponentType displayComponentType)
    {
        return ComponentDisplayType(AutoComponentDisplayType.DataGrid, displayComponentType);
    }
    
    #endregion
    
    #region Grid
    
    public IAutoComponentPropertyConfigurator GridType(AutoGridType gridType)
    {
        foreach (var type in AutoComponentDisplayTypeExtensions.GetValues())
        {
            CurrentComponentConfiguration.Grids[type].GridType = gridType;
        }
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator FlexType(AutoFlexType flexType)
    {
        foreach (var type in AutoComponentDisplayTypeExtensions.GetValues())
        {
            CurrentComponentConfiguration.Grids[type].FlexType = flexType;
        }
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator Grid(AutoComponentDisplayType type, AutoGridConfiguration gridConfiguration)
    {
        CurrentComponentConfiguration.Grids[type] = gridConfiguration;
        return this;
    }
    
    #endregion
    
    #region Specific Configurations
    
    public IAutoComponentPropertyConfigurator Size(AutoFieldSize size)
    {
        foreach (var showFlag in AutoComponentDisplayTypeExtensions.GetValues())
        {
            CurrentComponentConfiguration.Sizes[showFlag] = size;
        }
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator Important()
    {
        CurrentComponentConfiguration.Important = true;
        return this;
    }
    
    public IAutoComponentPropertyConfigurator AddExtraAttributes(Dictionary<string, object> metadata)
    {
        foreach (var (key, value) in metadata)
        {
            CurrentComponentConfiguration.ExtraAttributes.TryAdd(key, value);
        }
        
        return this;
    }
    
    public IAutoComponentPropertyConfigurator AddExtraAttribute(string name, object value)
    {
        CurrentComponentConfiguration.ExtraAttributes.TryAdd(name, value);
        return this;
    }
    
    #endregion
}