using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Logic;
using Client.Scripts.LogicModels;
using Client.Scripts.Presenters;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallPresenter _ballView;
        [SerializeField] private LinePresenter _lineView;
        [SerializeField] private BallCompressionPresenter _compressionView;
        [SerializeField] private BallRotationPresenter _rotationView;
        public override void InstallBindings()
        {
            BindModels();
            Container.Resolve<IGameFactory>().CreateEnemy();
        }

        private void BindModels()
        {
            Container.Bind<LinePresenter>().FromInstance(_lineView).AsSingle();
            Container.Bind<BallPresenter>().FromInstance(_ballView).AsSingle();
            Container.Bind<BallCompressionPresenter>().FromInstance(_compressionView).AsSingle();
            Container.Bind<BallRotationPresenter>().FromInstance(_rotationView).AsSingle();
            
            Container.BindInterfacesAndSelfTo<Line>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<Ball>().FromNew().AsSingle();
        }
    }
}
