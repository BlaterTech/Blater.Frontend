namespace Blater.Frontend.Client.Services;

public class StateNotifierService
{
    // Evento que notifica mudanças
    public event Action? StateChanged;

    // Método para disparar o evento
    public void NotifyStateChanged() => StateChanged?.Invoke();
}