using System.ComponentModel.DataAnnotations;

namespace Blater.Frontend.Attributes.Validations;

/// <summary>
///     International Fiscal Number
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FiscalNumberAttribute : ValidationAttribute
{
}