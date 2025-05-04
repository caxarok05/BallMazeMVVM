using Client.Scripts.Services;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class DataServiceInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            BindDataService();
        }

        private void BindDataService()
        {
            Container.BindInterfacesAndSelfTo<JsonDataService>().AsSingle().NonLazy();
        }
    }
}