using Serilog.Core;
using Serilog.Events;

using System.Diagnostics;

namespace Blater.Frontend.Logging;

public class InvocationContextEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (!logEvent.Properties.TryGetValue(Constants.SourceContextPropertyName, out var property))
        {
            return;
        }

        var sourceContext = ((ScalarValue)property).Value?.ToString();
        if (sourceContext == null)
        {
            return;
        }

        var callerFrame = GetCallerStackFrame(sourceContext);

        if (callerFrame == null)
        {
            return;
        }

        var methodName = callerFrame.GetMethod()?.Name;
        var lineNumber = callerFrame.GetFileLineNumber();
        var fileName = callerFrame?.GetFileName();

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CallerContext", sourceContext));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CallerMemberName", methodName));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CallerFilePath", fileName));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CallerLineNumber", lineNumber));
    }

    private static StackFrame? GetCallerStackFrame(string className)
    {
        var trace = new StackTrace(true);
        var frames = trace.GetFrames();

        var callerFrame = frames.FirstOrDefault(f => f.GetMethod()?.DeclaringType?.FullName == className);

        return callerFrame;
    }
}