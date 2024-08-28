using Blater.Frontend.Client.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Components;

public partial class AutoTimeline : ComponentBase
{
    [Parameter]
    public Dictionary<int, string> StepsAndTexts { get; set; } = null!;
    
    [Parameter]
    public int CurrentStep { get; set; }
    
    [Inject]
    public ILocalizationService LocalizationService { get; set; } = null!;
}