using Blater.Enumerations;
using Blater.Frontend.Client.Auto.AutoBuilders.Types;
using Blater.Frontend.Client.Interfaces;

namespace Blater.Frontend.Client.Translations;

public class PortugueseTranslation : ITranslation
{
    public int Priority => 1;
    public LanguageTranslation Language => LanguageTranslation.Portuguese;

    public Dictionary<string, string> Dictionary => new()
    {
        //NavMenu
        [nameof(LocalizationComponent.Logout)] = "Sair",
        [nameof(LocalizationComponent.HomePage)] = "Home",
        
        //Buttons
        [nameof(LocalizationComponent.GoBackButton)] = "Voltar",
        [nameof(LocalizationComponent.Processing)] = "Processando",
        [nameof(LocalizationComponent.EditButton)] = "Editar",
        [nameof(LocalizationComponent.SaveButton)] = "Salvar",
    };
}