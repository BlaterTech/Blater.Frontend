using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blater.FrontEnd.EasyRenderTree;

[SuppressMessage("Usage", "ASP0006:Do not use non-literal sequence numbers")]
public class EasyRenderTreeBuilder(RenderTreeBuilder renderTreeBuilder)
{
    public RenderTreeBuilder RenderTreeBuilder => renderTreeBuilder;

    public EasyRenderTreeComponent OpenComponent<T>() where T : IComponent
    {
        renderTreeBuilder.OpenComponent<T>(0);
        return new EasyRenderTreeComponent(renderTreeBuilder);
    }

    public EasyRenderTreeComponent OpenComponent(Type type)
    {
        renderTreeBuilder.OpenComponent(0, type);
        return new EasyRenderTreeComponent(renderTreeBuilder);
    }

    public EasyRenderTreeComponent OpenElement(string element)
    {
        renderTreeBuilder.OpenElement(0, element);
        return new EasyRenderTreeComponent(renderTreeBuilder, true);
    }
    
    public void OpenElement(string element, Action<EasyRenderTreeBuilder> buildContent)
    {
        renderTreeBuilder.OpenElement(0, element);
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(renderTreeBuilder);
        buildContent(easyRenderTreeBuilder);
        renderTreeBuilder.CloseElement();
    }

    public void AddContent(object? content)
    {
        renderTreeBuilder.AddContent(5, content);
    }
}