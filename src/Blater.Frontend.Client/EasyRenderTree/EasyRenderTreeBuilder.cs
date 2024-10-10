using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using System.Diagnostics.CodeAnalysis;

namespace Blater.Frontend.Client.EasyRenderTree;

[SuppressMessage("Usage", "ASP0006:Do not use non-literal sequence numbers")]
public class EasyRenderTreeBuilder(RenderTreeBuilder renderTreeBuilder)
{
    private int _sequenceIndex;

    public RenderTreeBuilder RenderTreeBuilder => renderTreeBuilder;

    public EasyRenderTreeComponent OpenComponent<T>() where T : IComponent
    {
        renderTreeBuilder.OpenComponent<T>(_sequenceIndex++);
        return new EasyRenderTreeComponent(renderTreeBuilder, _sequenceIndex);
    }

    public EasyRenderTreeComponent OpenComponent(Type type)
    {
        renderTreeBuilder.OpenComponent(_sequenceIndex++, type);
        return new EasyRenderTreeComponent(renderTreeBuilder, _sequenceIndex);
    }

    public EasyRenderTreeComponent OpenElement(string element)
    {
        renderTreeBuilder.OpenElement(_sequenceIndex++, element);
        return new EasyRenderTreeComponent(renderTreeBuilder, _sequenceIndex, true);
    }

    public void OpenElement(string element, Action<EasyRenderTreeBuilder> buildContent)
    {
        renderTreeBuilder.OpenElement(_sequenceIndex++, element);
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(renderTreeBuilder);
        buildContent(easyRenderTreeBuilder);
        renderTreeBuilder.CloseElement();
    }

    public void AddContent(object? content)
    {
        renderTreeBuilder.AddContent(_sequenceIndex++, content);
    }
}