using Client.Scripts.Services;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class ParticleBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ParticleBehaviourService>().AsSingle();
        }
    }
}
