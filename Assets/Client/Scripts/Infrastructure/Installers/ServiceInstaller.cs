using Zenject;

public class ServiceInstaller : MonoInstaller
{
    private AbstractInput _inputManager;

    public override void InstallBindings()
    { 
        
        InitInputManager();
        Container.Bind<AbstractInput>().FromInstance(_inputManager).AsSingle();
        _inputManager.Init();
        
    }

    private void InitInputManager()
    {

#if UNITY_ANDROID || UNITY_IOS
        _inputManager = new MobileInput();
#else
        _inputManager = new StandaloneInput();
#endif
    }
}
