using System.Linq.Expressions;

namespace Blater.Frontend.Models;

public class FindPropertiesAttributeOptions<TOption>
{
    public string? Title { get; set; }
    public string? ExtraClass { get; set; }

    /// <summary>
    /// The fields to ignore.
    /// </summary>
    public Expression<Func<TOption, object?>>[]? IgnoreFields { get; set; }
    /// <summary>
    /// The fields to only return.
    /// </summary>
    public Expression<Func<TOption, object?>>[]? OnlyFields { get; set; }
    
    /// <summary>
    /// The fields to be unique.
    /// </summary>
    public Expression<Func<TOption, object?>>[]? UniqueFields { get; set; }

    public List<string> IgnoreFieldsAsStrings => GetPropertyNames(IgnoreFields);
    public List<string> OnlyFieldsAsStrings => GetPropertyNames(OnlyFields);
    public List<string> UniqueFieldsAsStrings => GetPropertyNames(UniqueFields);

    private static List<string> GetPropertyNames(Expression<Func<TOption, object?>>[]? propertyExpressions)
    {
        if (propertyExpressions == null)
        {
            return [];
        }

        var propertyNames = new List<string>();
        foreach (var expression in propertyExpressions)
        {
            switch (expression.Body)
            {
                case MemberExpression memberExpression:
                    propertyNames.Add(memberExpression.Member.Name);
                    break;
                case UnaryExpression { Operand: MemberExpression unaryMemberExpression }:
                    propertyNames.Add(unaryMemberExpression.Member.Name);
                    break;
            }
        }

        return propertyNames;
    }
}