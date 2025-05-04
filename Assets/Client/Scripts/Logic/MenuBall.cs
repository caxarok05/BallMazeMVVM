using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Presenters;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Client.Scripts.Logic
{
    public class MenuBall : IInitializable, IDisposable
    {
        private readonly MenuBallPresenter _ballPresenter;
        private readonly BallCompressionPresenter _compressionPresenter;
        private readonly BallRotationPresenter _rotationPresenter;
        private readonly SignalBus _signalBus;
        private readonly CancellationTokenSource _tokenSource = new();

        public MenuBall(
            MenuBallPresenter ballPresenter,
            BallCompressionPresenter compressionPresenter,
            BallRotationPresenter rotationPresenter,
            SignalBus signalBus)
        {
            _ballPresenter = ballPresenter;
            _compressionPresenter = compressionPresenter;
            _rotationPresenter = rotationPresenter;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<HitBorder>(OnWallHit);
            SetRandomVelocity(_tokenSource.Token).Forget();
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<HitBorder>(OnWallHit);
            _tokenSource?.Cancel();
        }

        private void OnWallHit(HitBorder args)
        {
            _compressionPresenter.TryCompress(args.ballVelocity);
            Vector3 reflectedVelocity = CustomVelocity.ReflectVector(args.ballVelocity, args.contactPointNormal);
            _ballPresenter.SetVelocity(reflectedVelocity);
            _rotationPresenter.SetRotationVector(reflectedVelocity);
        }

        private async UniTaskVoid SetRandomVelocity(CancellationToken ct = default)
        {
            while (true)
            {
                await UniTask.WaitForSeconds(3, false, PlayerLoopTiming.Update, ct);
                Vector3 randomVelocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                _ballPresenter.SetVelocity(randomVelocity);
                _rotationPresenter.SetRotationVector(randomVelocity);
            }
        }
    }
}
