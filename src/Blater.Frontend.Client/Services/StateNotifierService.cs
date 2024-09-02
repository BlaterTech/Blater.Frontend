using System.Diagnostics.CodeAnalysis;

namespace Blater.Frontend.Client.Services;

[SuppressMessage("Design", "CA1000:Não declarar membros estáticos em tipos genéricos")]
public static class StateNotifierService<TValue>
{
    // Evento que notifica mudanças
    public static event Action<TValue>? StateChanged;

    // Método para disparar o evento
    public static void NotifyStateChanged(TValue value) => StateChanged?.Invoke(value);
}