namespace Client.Scripts.LogicModels
{
    public interface IStateMachineView
    {
        IState ActiveState { get; }
    }
}
