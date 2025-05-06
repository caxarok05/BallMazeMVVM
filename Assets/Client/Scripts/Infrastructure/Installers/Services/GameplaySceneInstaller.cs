using Client.Scripts.Infrastructure.AssetManagement;
using Client.Scripts.LogicModels;
using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private int _currentlevel;
        [SerializeField] private string _nextLevel;
        private AbstractInput _inputManager;
        public override void InstallBindings()
        {
            InitInputManager();
            Container.Bind<AbstractInput>().FromInstance(_inputManager).AsSingle();
            _inputManager.Init();

            BindGameFactory();
            BindMoneyService();
            BindHealthService();

            CreateEnvironment();
        }

        private void CreateEnvironment()
        {
            IGameFactory factory = Container.Resolve<IGameFactory>();

            factory.CreateEnemy();
            factory.CreateMoney();
            factory.CreateStartPoint();
            factory.CreateFinishPoint();
        }

        private void BindMoneyService()
        {
            Container.BindInterfacesAndSelfTo<MoneyService>().AsSingle().WithArguments(_currentlevel);
        }

        private void BindHealthService()
        {
            Container.BindInterfacesAndSelfTo<HealthService>().AsSingle().WithArguments(_currentlevel);
        }

        private void BindGameFactory()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle().WithArguments(_currentlevel, _nextLevel);
        }

        private void InitInputManager()
        {

#if UNITY_EDITOR
            _inputManager = new StandaloneInput();
#elif UNITY_ANDROID || UNITY_IOS
            _inputManager = new MobleInput();
#else
            _inputManager = new StandaloneInput();
#endif
        }
    }
}
