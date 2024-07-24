using System.ComponentModel.DataAnnotations;

namespace Blater.Frontend.Auto.AutoTable.Models;

public class ValidationConfiguration<TProperty>
{
    public List<ValidationAttribute> ValidationAttributes { get; set; } = [];
    public List<Func<TProperty, bool>> Rules { get; set; } = [];
    public List<string> ErrorMessages { get; set; } = [];
}