using Blater.Frontend.Client.Interfaces;
using Blater.Frontend.Client.Models;
using Blater.Interfaces;
using Blater.Models.Bases;
using Blater.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Blater.Frontend.Client.Components;

public partial class BlaterTable<T> : ComponentBase where T : BaseDataModel
{
    #region Parameters

    #region Events

    [Parameter]
    public EventCallback<T> OnDisabledChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnDetails { get; set; }

    [Parameter]
    public EventCallback<T> OnEditChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnDeleteChanged { get; set; }

    [Parameter]
    public EventCallback<T> OnAddChanged { get; set; }
    
    [Parameter]
    public EventCallback<T> OnFilterChanged { get; set; }

    [Parameter]
    public EventCallback<(T model, bool value)> OnCheckboxChanged { get; set; }

    [Parameter]
    public EventCallback OnCreate { get; set; }
    
    [Parameter]
    public EventCallback<HashSet<T>> OnSelectedItemsChanged { get; set; }
    
    [Parameter]
    public EventCallback<T> OnSelectedItemChanged { get; set; }

    #endregion
    
    [Parameter]
    public List<T> Items { get; set; } = [];

    [Parameter]
    public List<Guid> Ids { get; set; } = [];
    
    [Parameter]
    public bool CreateButton { get; set; } = true;

    [Parameter]
    public bool ShowDefaultActions { get; set; } = true;
    
    [Parameter]
    public bool ShowCustomActions { get; set; } = false;
    
    [Parameter]
    public List<CustomDataGridAction<T>> CustomDataGridActions { get; set; } = [];

    [Parameter]
    public bool MultiSelection { get; set; } = false;

    #endregion

    #region Injects

    [Inject]
    public ILogger<BlaterTable<T>> Logger { get; set; } = null!;

    [Inject]
    public IBlaterDatabaseStoreT<T> DatabaseStoreT { get; set; } = null!;

    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;

    [Inject]
    public INavigationService NavigationService { get; set; } = null!;

    #endregion
    
    public string Title { get; set; } = string.Empty;
    
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