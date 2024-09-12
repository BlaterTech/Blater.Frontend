using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineConfigurationBuilder : IAutoFormTimelineConfigurationBuilder
{
    private readonly AutoFormTimelineConfiguration _configuration;
    private readonly Type _type;

    public AutoFormTimelineConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoFormTimelineConfiguration configuration)
        {
            _configuration = configuration.FormTimelineConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoFormTimelineConfiguration");
        }
    }

    #region Group
    
    public IAutoFormTimelineMemberConfigurationBuilder AddGroup(AutoFormTimelineGroupConfiguration groupConfiguration)
        => AddGroup(AutoComponentDisplayType.Form, groupConfiguration);

    public IAutoFormTimelineMemberConfigurationBuilder AddGroupCreateOnly(AutoFormTimelineGroupConfiguration groupConfiguration)
        => AddGroup(AutoComponentDisplayType.FormCreate, groupConfiguration);

    public IAutoFormTimelineMemberConfigurationBuilder AddGroupEditOnly(AutoFormTimelineGroupConfiguration groupConfiguration)
        => AddGroup(AutoComponentDisplayType.FormEdit, groupConfiguration);

    #endregion

    #region Actions

    public IAutoFormTimelineConfigurationBuilder Actions(AutoFormTimelineActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.Form, actionConfiguration);

    public IAutoFormTimelineConfigurationBuilder ActionsCreateOnly(AutoFormTimelineActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormCreate, actionConfiguration);

    public IAutoFormTimelineConfigurationBuilder ActionsEditOnly(AutoFormTimelineActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormEdit, actionConfiguration);

    #endregion

    private AutoFormTimelineMemberConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, AutoFormTimelineGroupConfiguration groupConfiguration)
    {
        if (!_configuration.GroupConfigurations.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.GroupConfigurations.TryAdd(displayType, value);
        }

        var index = value.IndexOf(groupConfiguration);
        if (index != -1)
        {
            value[index] = groupConfiguration;
        }
        else
        {
            value.Add(groupConfiguration);
        }

        _configuration.GroupConfigurations[displayType] = value;
        
        return new AutoFormTimelineMemberConfigurationBuilder(_type, groupConfiguration);
    }

    private AutoFormTimelineConfigurationBuilder Actions(AutoComponentDisplayType displayType, AutoFormTimelineActionConfiguration actionConfiguration)
    {
        if (_configuration.ActionConfiguration.TryGetValue(displayType, out var value))
        {
            _configuration.ActionConfiguration[displayType] = actionConfiguration;
        }
        else
        {
            value = actionConfiguration;
            _configuration.ActionConfiguration.TryAdd(displayType, value);
        }
        
        return this;
    }
}