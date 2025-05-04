using Client.Scripts.Logic;
using Client.Scripts.Presenters;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class MenuBallInstaller : MonoInstaller
    {
        [SerializeField] private MenuBallPresenter _ballPresenter;
        [SerializeField] private BallCompressionPresenter _compressionPresenter;
        [SerializeField] private BallRotationPresenter _rotationPresenter;
        public override void InstallBindings()
        {
            BindPresenters();
        }

        private void BindPresenters()
        {

            Container.BindInterfacesAndSelfTo<MenuBallPresenter>().FromInstance(_ballPresenter).AsSingle();
            Container.Bind<BallCompressionPresenter>().FromInstance(_compressionPresenter).AsSingle();
            Container.Bind<BallRotationPresenter>().FromInstance(_rotationPresenter).AsSingle();

            Container.BindInterfacesAndSelfTo<MenuBall>().FromNew().AsSingle();
        }
    }
}
