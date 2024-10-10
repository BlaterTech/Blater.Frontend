using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blater.Frontend.Client.EasyRenderTree;

public class EasyRenderTreeComponent(RenderTreeBuilder renderTreeBuilder, int sequence, bool isElement = false)
{
    public EasyRenderTreeComponent AddAttribute(string name, object? value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return this;
        }

        renderTreeBuilder.AddAttribute(sequence, name, value);
        return this;
    }

    public EasyRenderTreeComponent AddComponentParameter(string name, object? value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return this;
        }

        renderTreeBuilder.AddComponentParameter(sequence, name, value);
        return this;
    }

    public EasyRenderTreeComponent AddChildContent(Action<EasyRenderTreeBuilder> buildContent)
    {
        renderTreeBuilder.AddAttribute(sequence, "ChildContent", (RenderFragment)(builder =>
        {
            var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);
            buildContent(easyRenderTreeBuilder);
        }));

        return this;
    }

    public EasyRenderTreeComponent AddContent(object content)
    {
        renderTreeBuilder.AddContent(sequence, content);
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