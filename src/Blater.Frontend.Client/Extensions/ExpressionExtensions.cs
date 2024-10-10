using System.Linq.Expressions;
using System.Reflection;

namespace Blater.Frontend.Client.Extensions;
public static class ExpressionExtensions
{
    public static PropertyInfo GetPropertyInfo<TModel, TPropertyType>(this Expression<Func<TModel, TPropertyType>> expression)
    {
        if (expression.Body is MemberExpression memberExpression && memberExpression.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo;
        }

        throw new ArgumentException("Expression is not a valid property expression.", nameof(expression));
    }
}
