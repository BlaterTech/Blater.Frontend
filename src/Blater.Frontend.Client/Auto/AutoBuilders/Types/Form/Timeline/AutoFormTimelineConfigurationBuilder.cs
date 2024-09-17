﻿using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form.Timeline;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineConfigurationBuilder<TModel> : IAutoFormTimelineConfigurationBuilder<TModel>
{
    private readonly AutoFormTimelineConfiguration _configuration;
    private readonly object _instance;
    public AutoFormTimelineConfigurationBuilder(object instance)
    {
        _instance = instance;
        if (_instance is IAutoFormTimelineConfiguration<TModel> configuration)
        {
            _configuration = configuration.FormTimelineConfiguration;
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoFormTimelineConfiguration<{typeof(TModel).Name}>");
        }
    }

    public AutoFormTimelineConfiguration AddStepCreateOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimelineCreate, action);
    public AutoFormTimelineConfiguration AddStepEditOnly(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimelineCreate, action);

    public AutoFormTimelineConfiguration AddStep(string title, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action)
        => AddStep(title, AutoComponentDisplayType.FormTimeline, action);
    
    private AutoFormTimelineConfiguration AddStep(string title, AutoComponentDisplayType displayType, Action<IAutoFormTimelineGroupConfigurationBuilder<TModel>> action)
    {
        if (!_configuration.Steps.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.Steps.TryAdd(displayType, value);
        }

        var existsValue = value.Exists(x => x.Value == title);
        if (!existsValue)
        {
            var item = value.LastOrDefault();

            int step;
            if (string.IsNullOrWhiteSpace(item?.Value))
            {
                step = 0;
            }
            else
            {
                step = item.Key + 1;
            }

            var newStep = new AutoFormTimelineStepConfiguration
            {
                Value = title,
                Key = step
            };
            value.Add(newStep);
            
            var builder = new AutoFormTimelineGroupConfigurationBuilder<TModel>(newStep.AutoFormConfiguration);
        
            action.Invoke(builder);
        }

        _configuration.Steps[displayType] = value;

        return _configuration;
    }
}