using Blater.Enumerations;
using Blater.Frontend.Client.Auto.AutoBuilders.Types;
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
    };
}