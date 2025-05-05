using Client.Scripts.Services;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class AdInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BannerAdService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InterstitialAdService>().AsSingle();
        }
    }
}
