namespace Client.Scripts.Logic
{
    public interface IStateMachineView
    {
        IState ActiveState { get; }
    }
}
