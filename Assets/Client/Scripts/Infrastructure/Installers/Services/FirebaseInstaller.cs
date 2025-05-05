using Client.Scripts.Services;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class FirebaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseInitialize>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsEventService>().AsSingle();
        }
    }
}
