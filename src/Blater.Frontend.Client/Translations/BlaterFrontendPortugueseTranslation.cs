using Blater.Enumerations;
using Blater.Frontend.Client.Auto.AutoBuilders.Types;
using Blater.Frontend.Client.Enumerations;
using Blater.Frontend.Client.Interfaces;

namespace Blater.Frontend.Client.Translations;

public class BlaterFrontendPortugueseTranslation : ITranslation
{
    public int Priority => -100;
    public LanguageTranslation Language => LanguageTranslation.Portuguese;

    public Dictionary<string, string> Dictionary => new()
    {
        //NavMenu
        [nameof(LocalizationComponent.Logout)] = "Sair",
        ["NavMenu-Home"] = "Home",

        //Buttons
        [nameof(LocalizationComponent.GoBackButton)] = "Voltar",
        [nameof(LocalizationComponent.Processing)] = "Processando",
        [nameof(LocalizationComponent.EditButton)] = "Editar",
        [nameof(LocalizationComponent.SaveButton)] = "Salvar",
        [nameof(LocalizationComponent.Collect)] = "Recolher",
        ["Blater-AutoFormTimeline-Button-Save"] = "Salvar",
        ["Blater-AutoFormTimeline-Button-Edit"] = "Editar",
        ["Blater-AutoFormTimeline-Button-Next"] = "Avançar",
        ["Blater-AutoFormTimeline-Button-GoBack"] = "Voltar",
        
        //AutoTableBuilder
        ["BlaterTable-DateRange-Label"] = "Período",
        ["BlaterTable-GlobalSearch-LabelName"] = "Buscar",
        ["BlaterTable-GlobalSearch-Placeholder"] = "Buscar informações",

        //AutoBadge
        [$"Badge-{nameof(StatusBadgeType.Available)}"] = "Disponível",
        [$"Badge-{nameof(StatusBadgeType.Loading)}"] = "Carregando",
        [$"Badge-{nameof(StatusBadgeType.Processing)}"] = "Em processamento",
        [$"Badge-{nameof(StatusBadgeType.InUse)}"] = " Em uso",
        [$"Badge-{nameof(StatusBadgeType.Active)}"] = "Ativo",
        [$"Badge-{nameof(StatusBadgeType.Overdue)}"] = "Vencido",
        [$"Badge-{nameof(StatusBadgeType.Pending)}"] = "Pendente",
        [$"Badge-{nameof(StatusBadgeType.InProgress)}"] = "Em andamento",
        [$"Badge-{nameof(StatusBadgeType.Concluded)}"] = "Concluído",
        [$"Badge-{nameof(StatusBadgeType.Unknown)}"] = "Desconhecido",
    };
}