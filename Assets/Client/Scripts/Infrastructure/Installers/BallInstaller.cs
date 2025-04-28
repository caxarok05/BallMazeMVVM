using Client.Scripts.LogicModels;
using Client.Scripts.LogicViews;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallView _ballView;
        [SerializeField] private LineView _lineView;
        [SerializeField] private BallCompressionView _compressionView;
        [SerializeField] private BallRotationView _rotationView;
        public override void InstallBindings()
        {
            BindModels();
        }

        private void BindModels()
        {
            Container.Bind<LineView>().FromInstance(_lineView).AsSingle();
            Container.Bind<BallView>().FromInstance(_ballView).AsSingle();
            Container.Bind<BallCompressionView>().FromInstance(_compressionView).AsSingle();
            Container.Bind<BallRotationView>().FromInstance(_rotationView).AsSingle();
            
            Container.BindInterfacesAndSelfTo<LineModel>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<BallModel>().FromNew().AsSingle();
        }
    }
}
