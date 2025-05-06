using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Presenters;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Client.Scripts.Logic
{
    public class MenuBall : BaseBall
    {
        private readonly MenuBallPresenter _ballPresenter;
        private readonly CancellationTokenSource _tokenSource = new();

        public MenuBall(
            MenuBallPresenter ballPresenter,
            BallCompressionPresenter compressionPresenter,
            BallRotationPresenter rotationPresenter,
            SignalBus signalBus) : base(compressionPresenter, rotationPresenter, signalBus)
        {
            _ballPresenter = ballPresenter;
        }

        public override void Initialize()
        {
            _signalBus.Subscribe<HitBorder>(OnWallHit);
            SetRandomVelocity(_tokenSource.Token).Forget();
        }

        public override void Dispose()
        {
            _signalBus.TryUnsubscribe<HitBorder>(OnWallHit);
            _tokenSource?.Cancel();
        }

        private void OnWallHit(HitBorder args)
        {
            base.OnWallHit(args, _ballPresenter);
        }
        private async UniTaskVoid SetRandomVelocity(CancellationToken ct = default)
        {
            while (!ct.IsCancellationRequested)
            {
                await UniTask.WaitForSeconds(3, false, PlayerLoopTiming.Update, ct);
                Vector3 randomVelocity = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                _ballPresenter.SetVelocity(randomVelocity);
                _rotationPresenter.SetRotationVector(randomVelocity);
            }
        }
    }
}
