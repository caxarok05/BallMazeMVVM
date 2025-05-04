using Client.Scripts.Infrastructure.Signals;
using MVVM;
using System;
using UniRx;
using Zenject;

namespace Client.Scripts.UI 
{ 
    public class BallStatViewModel : IInitializable, IDisposable
    {
        [Data("Speed")]
        public readonly ReactiveProperty<string> Speed = new();

        [Data("Rotation")]
        public readonly ReactiveProperty<string> Rotation = new();

        private readonly SignalBus _signalBus;
        public BallStatViewModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            ChangeRotation(new BallRotationChanged(0));
            ChangeSpeed(new BallSpeedChanged(0));
            
            _signalBus.Subscribe<BallRotationChanged>(ChangeRotation);
            _signalBus.Subscribe<BallSpeedChanged>(ChangeSpeed);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<BallRotationChanged>(ChangeRotation);
            _signalBus.TryUnsubscribe<BallSpeedChanged>(ChangeSpeed);
        }

        private void ChangeSpeed(BallSpeedChanged args) => Speed.Value = $"Speed: {Math.Round(args.Speed, 2)}";
        private void ChangeRotation(BallRotationChanged args) => Rotation.Value = $"Rotation: {Math.Round(args.Rotation, 2)}";

    }
}
