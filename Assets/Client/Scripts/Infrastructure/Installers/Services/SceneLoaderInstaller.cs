using Client.Scripts.Presenters;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class SceneLoaderInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _presenter;
        public override void InstallBindings()
        {
            CurtainPresenter curtain = Container.InstantiatePrefabForComponent<CurtainPresenter>(_presenter);
            Container.Bind<CurtainPresenter>().FromInstance(curtain).AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
        }
    }
}
