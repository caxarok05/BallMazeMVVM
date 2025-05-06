using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Presenters;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Logic
{
    public abstract class BaseBall : IInitializable, IDisposable
    {
        protected readonly BallCompressionPresenter _compressionPresenter;
        protected readonly BallRotationPresenter _rotationPresenter;
        protected readonly SignalBus _signalBus;
        public BaseBall(
        BallCompressionPresenter compressionPresenter,
        BallRotationPresenter rotationPresenter,
        SignalBus signalBus)
        {
            _compressionPresenter = compressionPresenter;
            _rotationPresenter = rotationPresenter;
            _signalBus = signalBus;
        }
        public abstract void Initialize();
        public abstract void Dispose();

        protected void OnWallHit(HitBorder args, IBallPresenter ballPresenter)
        {
            _compressionPresenter.TryCompress(args.ballVelocity);
            Vector3 reflectedVelocity = CustomVelocity.ReflectVector(args.ballVelocity, args.contactPointNormal);
            ballPresenter.SetVelocity(reflectedVelocity);
            _rotationPresenter.SetRotationVector(reflectedVelocity);
        }
    }
}
