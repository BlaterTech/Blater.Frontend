using Blater.Utilities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blater.Frontend.Client.Components;

public partial class BlaterTable : ComponentBase
{
    public record Employee(Guid Id, string Name, string Position, int YearsEmployed, int Salary, int Rating, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt, bool Enabled);

    public IEnumerable<Employee> Employees { get; set; } = [];
    public IEnumerable<Employee> FilterEmployees { get; set; } = [];

    private MudTable<Employee> _mudTable = null!;
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        
        await Task.Delay(1500);
        
        var random = new Random();
        var positions = new List<string>
        {
            "CPA", "Product Manager", "Developer", "IT Director", "Designer", "Analyst"
        };
        var employees = new List<Employee>();
        
        for (var i = 0; i < 100; i++)
        {
            var position = positions[random.Next(positions.Count)];
            var name = $"Employee {position} {i + 1}";
            var experience = random.Next(1, 21);
            var salary = random.Next(50_000, 250_001);
            var performanceRating = random.Next(1, 6);
            var id = SequentialGuidGenerator.NewGuid();
            var createdAt = DateTimeOffset.UtcNow;
            var updatedAt = DateTimeOffset.UtcNow;
            var enabled = true;
            employees.Add(new Employee(id, name, position, experience, salary, performanceRating, createdAt, updatedAt, enabled));
        }
        
        Employees = employees;

        FilterEmployees = Employees;

        _loading = false;
    }

    private void QueryFilter(object? value, string propertyName)
    {
        try
        {
            if (value == null || string.IsNullOrWhiteSpace(propertyName))
            {
                FilterEmployees = Employees;
                return;
            }
            
            var type = value.GetType();
            string stringValue;
            if (type == typeof(string))
            {
                stringValue = value.ToString()!;
            }
            else
            {
                var defaultValue = Activator.CreateInstance(type);

                if (value.Equals(defaultValue))
                {
                    FilterEmployees = Employees;
                    return;
                }

                stringValue = value.ToString()!;
            }
            
            var prop = typeof(Employee).GetProperty(propertyName);
            if (prop == null)
            {
                FilterEmployees = Employees;
                return;
            }

            FilterEmployees = Employees
                             .Where(e => prop.GetValue(e)?.ToString()?.Contains(stringValue, StringComparison.OrdinalIgnoreCase) == true)
                             .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            FilterEmployees = Employees;
        }
        finally
        {
            StateHasChanged();
        }
    }
}