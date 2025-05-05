using Client.Scripts.UI;
using Zenject;

namespace Assets.Client.Scripts.Infrastructure.Installers
{
    public class MenuViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuViewModel>().AsSingle().NonLazy();
        }
    }
}
