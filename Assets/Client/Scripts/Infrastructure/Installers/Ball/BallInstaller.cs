using Client.Scripts.Logic;
using Client.Scripts.Presenters;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private int _currentLevel = 0;
        [SerializeField] private GameBallPresenter _ballPresenter;
        [SerializeField] private LinePresenter _linePresenter;
        [SerializeField] private BallCompressionPresenter _compressionPresenter;
        [SerializeField] private BallRotationPresenter _rotationPresenter;
        [SerializeField] private BallRespawnPresenter _respawnPresenter;
        public override void InstallBindings()
        {
            Container.Bind<int>().FromInstance(_currentLevel).WhenInjectedInto<BallRespawnPresenter>();
            BindPresenters();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<GameBallPresenter>().FromInstance(_ballPresenter).AsSingle();
            Container.BindInterfacesAndSelfTo<BallRespawnPresenter>().FromInstance(_respawnPresenter).AsSingle();
            Container.Bind<LinePresenter>().FromInstance(_linePresenter).AsSingle();
            Container.Bind<BallCompressionPresenter>().FromInstance(_compressionPresenter).AsSingle();
            Container.Bind<BallRotationPresenter>().FromInstance(_rotationPresenter).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameBall>().FromNew().AsSingle();
        }
    }
}
