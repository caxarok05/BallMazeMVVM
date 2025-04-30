using Client.Scripts.LogicModels;
using Client.Scripts.Services;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        private AbstractInput _inputManager;
        public HedgehogView hedgehogView;

        public override void InstallBindings()
        {
            BindDataService();
            InitInputManager();
            Container.Bind<AbstractInput>().FromInstance(_inputManager).AsSingle();
            _inputManager.Init();
            BindGameFactory();
        }
        
        private void BindGameFactory()
        {
            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle().WithArguments(hedgehogView);
        }

        private void BindDataService()
        {
            Container.BindInterfacesAndSelfTo<JsonDataService>().AsSingle().NonLazy();
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
}