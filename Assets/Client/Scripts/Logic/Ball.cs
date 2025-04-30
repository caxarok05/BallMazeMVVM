using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Presenters;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Logic
{
    public class Ball : IInitializable, IDisposable
    {
        private readonly BallPresenter _ballPresenter;
        private readonly BallCompressionPresenter _compressionPresenter;
        private readonly LinePresenter _linePresenter;
        private readonly BallRotationPresenter _rotationPresenter;
        private readonly SignalBus _signalBus;

        public Ball(
            BallPresenter ballPresenter,
            LinePresenter linePresenter,
            BallCompressionPresenter compressionPresenter,
            BallRotationPresenter rotationPresenter,
            SignalBus signalBus)
        {
            _ballPresenter = ballPresenter;
            _linePresenter = linePresenter;
            _compressionPresenter = compressionPresenter;
            _rotationPresenter = rotationPresenter;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            _signalBus.Subscribe<OnBallClickedDown>(ShowLine);
            _signalBus.Subscribe<OnBallClickedUp>(HideLine);
            _signalBus.Subscribe<HitBorder>(OnWallHit);
            _signalBus.Subscribe<RequestBallPosition>(SetBallPosition);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnBallClickedDown>(ShowLine);
            _signalBus.TryUnsubscribe<OnBallClickedUp>(HideLine);
            _signalBus.TryUnsubscribe<HitBorder>(OnWallHit);
            _signalBus.TryUnsubscribe<RequestBallPosition>(SetBallPosition);
        }

        public void SetBallPosition()
        {
            _linePresenter.SetBallPosition(_ballPresenter.BallPosition);
        }

        public void ShowLine()
        {
            _linePresenter.SetLineOn();
        }

        public void HideLine()
        {
            _linePresenter.SetLineOff();
        }

        private void OnWallHit(HitBorder args)
        {
            _compressionPresenter.TryCompress(args.ballVelocity);
            Vector3 reflectedVelocity = CustomVelocity.ReflectVector(args.ballVelocity, args.contactPointNormal);
            _ballPresenter.SetVelocity(reflectedVelocity);
            _rotationPresenter.SetRotationVector(reflectedVelocity);

        }
    }
}
