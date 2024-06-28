using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blater.FrontEnd.EasyRenderTree;

[SuppressMessage("Usage", "ASP0006:Do not use non-literal sequence numbers")]
public class EasyRenderTreeComponent(RenderTreeBuilder renderTreeBuilder, bool isElement = false)
{
    public EasyRenderTreeComponent AddAttribute(string name, object? value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return this;
        }
        renderTreeBuilder.AddAttribute(1, name, value);
        return this;
    }
    
    public EasyRenderTreeComponent AddComponentParameter(string name, object? value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return this;
        }
        renderTreeBuilder.AddComponentParameter(1, name, value);
        return this;
    }

    public EasyRenderTreeComponent AddChildContent(Action<EasyRenderTreeBuilder> buildContent)
    {
        renderTreeBuilder.AddAttribute(2, "ChildContent", (RenderFragment)(builder =>
        {
            var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);
            buildContent(easyRenderTreeBuilder);
        }));

        return this;
    }

    public EasyRenderTreeComponent AddContent(object content)
    {
        renderTreeBuilder.AddContent(7, content);
        return this;
    }

    public EasyRenderTreeComponent AddContent(Action<EasyRenderTreeBuilder> buildContent)
    {
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(renderTreeBuilder);
        buildContent(easyRenderTreeBuilder);
        return this;
    }

    public void Close()
    {
        if (isElement)
        {
            renderTreeBuilder.CloseElement();
        }
        else
        {
            renderTreeBuilder.CloseComponent();
        }
    }
}