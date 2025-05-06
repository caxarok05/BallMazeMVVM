using Client.Scripts.UI;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class GameViewModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MoneyViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DeathPanelViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BallStatViewModel>().AsSingle().NonLazy();
        }
    }
}
