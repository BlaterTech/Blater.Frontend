using Blater.Frontend.AutoModelConfigurations.Configs;
using Blater.Frontend.Enumerations.AutoModel;

namespace Blater.Frontend.AutoModelConfigurations.Interfaces;

public interface IAutoComponentPropertyConfigurator
{
    public AutoComponentConfiguration CurrentComponentConfiguration { get; }
    
    /// <summary>
    ///     Sets the order on all show flag
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    IAutoComponentPropertyConfigurator Order(int order);
    
    IAutoComponentPropertyConfigurator Order(AutoComponentDisplayType displayType, int order);
    IAutoComponentPropertyConfigurator FormOrder(int order);
    IAutoComponentPropertyConfigurator FormEditOrder(int order);
    IAutoComponentPropertyConfigurator FormCreateOrder(int order);
    IAutoComponentPropertyConfigurator FilterOrder(int order);
    IAutoComponentPropertyConfigurator Table(int order, AutoComponentType displayComponentType);
    IAutoComponentPropertyConfigurator TableOrder(int order);
    IAutoComponentPropertyConfigurator DetailsOrder(int order);
    IAutoComponentPropertyConfigurator DetailsSize(AutoFieldSize autoFieldSize);
    IAutoComponentPropertyConfigurator FormSize(AutoFieldSize autoFieldSize);
    IAutoComponentPropertyConfigurator FormCreateSize(AutoFieldSize autoFieldSize);
    IAutoComponentPropertyConfigurator FormEditSize(AutoFieldSize autoFieldSize);
    IAutoComponentPropertyConfigurator TableSize(AutoFieldSize autoFieldSize);
    IAutoComponentPropertyConfigurator FormDisplayType(AutoFormInputType displayType);
    IAutoComponentPropertyConfigurator FormEditDisplayType(AutoFormInputType displayType);
    IAutoComponentPropertyConfigurator FormCreateDisplayType(AutoFormInputType displayType);
    IAutoComponentPropertyConfigurator FilterDisplayType(AutoFormInputType componentType);
    IAutoComponentPropertyConfigurator DetailsDisplayType(AutoComponentType displayComponentType);
    IAutoComponentPropertyConfigurator TableDisplayType(AutoComponentType displayComponentType);
    IAutoComponentPropertyConfigurator GridType(AutoGridType gridType);
    IAutoComponentPropertyConfigurator FlexType(AutoFlexType flexType);
    IAutoComponentPropertyConfigurator Grid(AutoComponentDisplayType type, AutoGridConfiguration gridConfiguration);
    IAutoComponentPropertyConfigurator Size(AutoFieldSize size);
    IAutoComponentPropertyConfigurator Important();
    IAutoComponentPropertyConfigurator AddExtraAttributes(Dictionary<string, object> metadata);
    IAutoComponentPropertyConfigurator AddExtraAttribute(string name, object value);
    IAutoComponentPropertyConfigurator LabelName(string labelName);
    
    IAutoComponentPropertyConfigurator HasParent(IAutoComponentPropertyConfigurator parent);
    
    IAutoComponentPropertyConfigurator SeparateCard();
    IAutoComponentPropertyConfigurator SeparateComponent();
}