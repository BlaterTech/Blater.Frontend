namespace Blater.Frontend.StateManagement;

public interface IStateComponent
{
    string ComponentStateId { get; }

    Task ReRender();
}