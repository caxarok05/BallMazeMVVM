using Client.Scripts.Infrastructure.Signals;
using Zenject;

namespace Client.Scripts.Infrastructure.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBallSignalBus();
        }
        private void BindBallSignalBus()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnBallClickedDown>();
            Container.DeclareSignal<OnBallClickedUp>();
            Container.DeclareSignal<RequestBallPosition>();
            Container.DeclareSignal<HitBorder>();
        }
    }
}
